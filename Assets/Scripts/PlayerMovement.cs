using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

// TODO

// Features
// Dash, SFX

// Known Bugs
// Weird Speed Changes, change direction needs to transfer to a normalized version of velocity when changing to a diagonal direction
// Pressing three keys in order and then releasing the middle one sets the direction to the first one when it should set it to just the last one
// Animation Not Matching Movement, could be caused/fixe by above

public class PlayerMovement : MonoBehaviour
{
    // Components
    Rigidbody2D rigidBody;
    Animator animator;
    TrailRenderer trailRenderer;

    // Inspector Variables
    [Foldout("Settings")]
    [SerializeField] float maxSpeed;

    [SerializeField] float accelerationTime;
    [SerializeField] AnimationCurve accelerationCurve;

    [SerializeField] float decelerationTime;
    [SerializeField] AnimationCurve decelerationCurve;

    [SerializeField] float dashSpeed;
    [EndFoldout]

    // Other Variables
    private List<KeyCode> currentDirectionKeys = new List<KeyCode>();
    private List<KeyCode> currentDirection = new List<KeyCode>();

    private bool walking;

    private void Start()
    {
        // Get Component References
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();

        // Add Listeners To Events
        InputHandler.current.onPressKey += KeyPressed;
        InputHandler.current.onReleaseKey += KeyReleased;
    }

    void FixedUpdate()
    {
        MovementCheck();
    }

    #region Input
    private void KeyPressed(KeyCode keyCode) // When a direction key is pressed it is added to the current direction keys
    {
        switch (keyCode)
        {
            case KeyCode.W:
                currentDirectionKeys.Add(KeyCode.W);
                break;
            case KeyCode.A:
                currentDirectionKeys.Add(KeyCode.A);
                break;
            case KeyCode.S:
                currentDirectionKeys.Add(KeyCode.S);
                break;
            case KeyCode.D:
                currentDirectionKeys.Add(KeyCode.D);
                break;
            case KeyCode.LeftShift:
                Dash();
                break;
            default:
                break;
        }

        SetCurrentDirection();
    }

    private void KeyReleased(KeyCode keyCode) // When a direction key is released it is removed from the current direction keys
    {
        switch (keyCode)
        {
            case KeyCode.W:
                currentDirectionKeys.Remove(KeyCode.W);
                break;
            case KeyCode.A:
                currentDirectionKeys.Remove(KeyCode.A);
                break;
            case KeyCode.S:
                currentDirectionKeys.Remove(KeyCode.S);
                break;
            case KeyCode.D:
                currentDirectionKeys.Remove(KeyCode.D);
                break;
        }

        SetCurrentDirection();
    }

    private void MovementCheck() // Check if player is pressing any direction keys
    {
        if (currentDirectionKeys.Count > 0) // If they are switch to walking animations
        {
            animator.SetBool("Moving", true);
        }

        else // If they arent switch to idle animations
        {
            animator.SetBool("Moving", false);
        }
    }

    private void SetCurrentDirection() // Set the current direction
    {
        Vector2 previousDirection = GetCurrentDirectionVector();

        if (currentDirectionKeys.Count > 1) // If there are atleast 2 direction keys currently being pressed, set the direction to the two latest ones
        {
            currentDirection.Clear();
            currentDirection.Add(currentDirectionKeys[currentDirectionKeys.Count - 1]);
            currentDirection.Add(currentDirectionKeys[currentDirectionKeys.Count - 2]);

            ChangeDirection(previousDirection, GetCurrentDirectionVector());

            if (!walking)
            {
                Accelerate();
                walking = true;
            }
        }

        else if (currentDirectionKeys.Count > 0) // Else if theres only 1 set it to that
        {
            currentDirection.Clear();
            currentDirection.Add(currentDirectionKeys[currentDirectionKeys.Count - 1]);

            ChangeDirection(previousDirection, GetCurrentDirectionVector());

            if (!walking)
            {
                Accelerate();
            }
        }

        else // If theres is none the current direction remains the same
        {
            if (walking)
            {
                Decelerate();
            }
        }

        

        UpdateAnimatorDirection();
    }

    public List<KeyCode> GetCurrentDirection()
    {
        return currentDirection;
    }

    public Vector2 GetCurrentDirectionVector()
    {
        Vector2 directionVector = new Vector2();

        for (int i = 0; i < currentDirection.Count; i++) // For each direction the current direction consists of (1-2)
        {
            switch (currentDirection[i]) // Depending on the direction set the direction of the vector
            {
                case KeyCode.W:
                    directionVector.y = 1;
                    break;
                case KeyCode.A:
                    directionVector.x = -1;
                    break;
                case KeyCode.S:
                    directionVector.y = -1;
                    break;
                case KeyCode.D:
                    directionVector.x = 1;
                    break;
            }
        }

        return directionVector;
    }

    #endregion

    #region Movement
    private void Move()
    {
        //accelerate quickly to max speed
        //move at max speed in current direction
        // decelerate quick but a bit slower to 0
    }

    [Button]
    private void Accelerate()
    {
        //Debug.Log("Accelerate");
        StartCoroutine(Accelerate(rigidBody.velocity, accelerationTime, accelerationCurve));
    }

    private IEnumerator Accelerate(Vector2 startVelocity, float duration, AnimationCurve curve)
    {
        float speed = 0;
        float timeElapsed = 0;
        walking = true;

        while (timeElapsed < duration)
        {
            if (currentDirectionKeys.Count == 0)
            {
                yield break;
            }

            speed = curve.Evaluate(Mathf.InverseLerp(0, duration, timeElapsed));
            rigidBody.velocity = Vector2.Lerp(startVelocity, GetCurrentDirectionVector().normalized * maxSpeed, speed);
            timeElapsed += Time.deltaTime;
            Debug.Log("mag + " + rigidBody.velocity.magnitude);
            yield return new WaitForFixedUpdate();
        }

        rigidBody.velocity = GetCurrentDirectionVector().normalized * maxSpeed;
    }

    [Button]
    private void Decelerate()
    {
        //Debug.Log("Decelerate");
        StartCoroutine(Decelerate(rigidBody.velocity, decelerationTime, decelerationCurve));
    }

    private IEnumerator Decelerate(Vector2 startVelocity, float duration, AnimationCurve curve)
    {
        float speed = 0;
        float timeElapsed = 0;
        walking = false;

        while (timeElapsed < duration)
        {
            speed = curve.Evaluate(Mathf.InverseLerp(0, duration, timeElapsed));
            rigidBody.velocity = Vector2.Lerp(startVelocity, Vector2.zero, speed);
            timeElapsed += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        rigidBody.velocity = Vector2.zero;
    }

    private void ChangeDirection(Vector2 previousDirection, Vector2 newDirection)
    {
        if (previousDirection != newDirection)
        {
            rigidBody.velocity = newDirection * Mathf.Max(Mathf.Abs(rigidBody.velocity.x), Mathf.Abs(rigidBody.velocity.y));
        }
    }

    private void Dash()
    {
        // move set distance in current direction faster than normal speed
    }
    #endregion

    #region Animation
    private void UpdateAnimatorDirection() // Update Animator bools so it can change to correct state
    {
        animator.SetBool("W", false);
        animator.SetBool("A", false);
        animator.SetBool("S", false);
        animator.SetBool("D", false);

        for (int i = 0; i < currentDirection.Count; i++) // For each direction the current direction consists of (1-2)
        {
            switch (currentDirection[i]) // Depending on the direction
            {
                case KeyCode.W:
                    animator.SetBool("W", true); // Set animator direction bool to true
                    break;

                case KeyCode.A:
                    animator.SetBool("A", true);
                    break;

                case KeyCode.S:
                    animator.SetBool("S", true);
                    break;

                case KeyCode.D:
                    animator.SetBool("D", true);
                    break;
            }
        }
    }
    #endregion
}