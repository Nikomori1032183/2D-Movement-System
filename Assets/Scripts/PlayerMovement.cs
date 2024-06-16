using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

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

    //[Foldout("Info")]
    //[SerializeField] Vector2 velocity = new Vector2(0, 0);
    //[EndFoldout]

    // Other Variables
    private List<KeyCode> currentDirectionKeys = new List<KeyCode>();
    private List<KeyCode> currentDirection = new List<KeyCode>();
    float timeElapsed;

    private void Start()
    {
        // Get Component References
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();

        // Add Listeners To Events
        InputHandler.current.onPressKey += KeyPressed;
        InputHandler.current.onReleaseKey += KeyReleased;

        timeElapsed = 0;
    }

    void FixedUpdate()
    {
        DirectionInputCheck();
        
        Move();
        LerpVelocity2(Vector2.zero, new Vector2(0,1) * maxSpeed, accelerationTime, accelerationCurve);
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

    private void SetCurrentDirection() // Set the current direction
    {
        if (currentDirectionKeys.Count > 1) // If there are atleast 2 direction keys currently being pressed, set the direction to the two latest ones
        {
            currentDirection.Clear();
            currentDirection.Add(currentDirectionKeys[currentDirectionKeys.Count - 1]);
            currentDirection.Add(currentDirectionKeys[currentDirectionKeys.Count - 2]);
        }

        else if (currentDirectionKeys.Count > 0) // Else if theres only 1 set it to that
        {
            currentDirection.Clear();
            currentDirection.Add(currentDirectionKeys[currentDirectionKeys.Count - 1]);

        }

        // If theres is none the current direction remains the same

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

    private void DirectionInputCheck() // Check if player should be moving
    {
        if (currentDirectionKeys.Count > 0) // If any direction keys are being pressed, Set animator moving bool to true & run Move()
        {
            animator.SetBool("Moving", true);
            //Move();
        }

        else // Else, Set animator moving bool to false
        {
            animator.SetBool("Moving", false);
        }
    }
    #endregion

    #region Movement
    private void Move()
    {
        //rigidBody.velocity = velocity;

        //Vector2.SmoothDamp(new Vector2(0, 0), new Vector2(1, 0), ref velocity, 1f);
        //rigidBody.velocity = velocity;


        //accelerate quickly to max speed
        //move at max speed in current direction
        // decelerate quick but a bit slower to 0
    }
    private IEnumerator LerpVelocity(Vector2 startVelocity, Vector2 endVelocity, float duration, AnimationCurve curve)
    {
        
        {
            float t = timeElapsed / duration;

            t = curve.Evaluate(t); 

            rigidBody.velocity = Vector2.Lerp(startVelocity, endVelocity, t);
            timeElapsed += Time.deltaTime;
        }

        //rigidBody.velocity = endVelocity;

        yield return null;
    }


    private void LerpVelocity2(Vector2 startVelocity, Vector2 endVelocity, float duration, AnimationCurve curve)
    {
        float speed;

        
        if (timeElapsed < duration)
        {
            Debug.Log(Mathf.InverseLerp(0, duration, timeElapsed));
            speed = curve.Evaluate(Mathf.InverseLerp(0, duration, timeElapsed));
            rigidBody.velocity = (new Vector2(0,1) * maxSpeed) * speed;
            timeElapsed += Time.deltaTime;
        }

        //yield return null;
    }

    [Button]
    private void Accelerate()
    {
        //timeElapsed = 0;
        //LerpVelocity2(rigidBody.velocity, GetCurrentDirectionVector() * maxSpeed, accelerationTime, accelerationCurve));
        //StartCoroutine(LerpVelocity2(rigidBody.velocity, GetCurrentDirectionVector() * maxSpeed, accelerationTime, accelerationCurve));
    }

    [Button]
    private void Decelerate()
    {
        //StartCoroutine(LerpVelocity2(rigidBody.velocity, Vector2.zero, decelerationTime, decelerationCurve));
    }

    private void ChangeDirection()
    {

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