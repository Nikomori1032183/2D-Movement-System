using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidBody;
    Animator animator;
    TrailRenderer trailRenderer;

    [SerializeField] float maxMoveSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    [SerializeField] float dashSpeed;

    private List<KeyCode> currentDirectionKeys = new List<KeyCode>();
    private List<KeyCode> currentDirection = new List<KeyCode>();

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();

        InputHandler.current.onPressKey += KeyPressed;
        InputHandler.current.onReleaseKey += KeyReleased;
    }

    void Update()
    {
        MovementCheck();
    }

    private void MovementCheck()
    {
        if (currentDirectionKeys.Count > 0)
        {
            animator.SetBool("Moving", true);
            Move();
        }

        else
        {
            animator.SetBool("Moving", false);
        }
    }

    private void UpdateAnimatorDirection()
    {
        animator.SetBool("W", false);
        animator.SetBool("A", false);
        animator.SetBool("S", false);
        animator.SetBool("D", false);

        for (int i = 0; i < currentDirection.Count; i++)
        {
            switch (currentDirection[i])
            {
                case KeyCode.W:
                    animator.SetBool("W", true);
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

    private void SetCurrentDirection()
    {
        if (currentDirectionKeys.Count > 1)
        {
            currentDirection.Clear();
            currentDirection.Add(currentDirectionKeys[currentDirectionKeys.Count - 1]);
            currentDirection.Add(currentDirectionKeys[currentDirectionKeys.Count - 2]);
        }
        
        else if (currentDirectionKeys.Count > 0)
        {
            currentDirection.Clear();
            currentDirection.Add(currentDirectionKeys[currentDirectionKeys.Count - 1]);
        }

        UpdateAnimatorDirection();
    }

    //private IEnumerator DirectionBuffer()
    //{
    //    directionBuffering = true;
    //    yield return new WaitForSeconds(0.5f);
    //    directionBuffering = false;
    //}

    public List<KeyCode> GetCurrentDirection()
    {
        return currentDirection;
    }

    private void KeyPressed(KeyCode keyCode)
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

    private void KeyReleased(KeyCode keyCode)
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

    private void Move()
    {
        if (InputHandler.current.IsKeyHeld(KeyCode.W))
        {
            if (InputHandler.current.IsKeyHeld(KeyCode.A))
            {

            }

            else if (InputHandler.current.IsKeyHeld(KeyCode.D))
            {

            }
        }
    }

    private void Dash()
    {
        // move set distance in current direction faster than normal speed
    }
}