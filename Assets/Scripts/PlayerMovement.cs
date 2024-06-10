using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidBody;
    Animator animator;

    [SerializeField] float walkSpeed;
    [SerializeField] float dashSpeed;

    enum ActionState
    {
        None, Walking, Dashing
    }

    ActionState actionState;

    enum DirectionState
    {
        North, North_East, East, South_East, South, South_West, West, North_West
    }

    DirectionState directionState;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        InputCheck();
        Movement();
    }

    private void InputCheck()
    {
        List<KeyCode> currentKeys = InputHandler.current.GetCurrentKeys();

        if (currentKeys.Contains(KeyCode.W))
        {
            actionState = ActionState.Walking;
            if (currentKeys.Contains(KeyCode.A))
            {
                directionState = DirectionState.North_West;
            }

            if (currentKeys.Contains(KeyCode.D))
            {
                directionState = DirectionState.North_East;
            }

            else
            {
                directionState = DirectionState.North;
            }
        }

        else if (currentKeys.Contains(KeyCode.S))
        {
            actionState = ActionState.Walking;
            if (currentKeys.Contains(KeyCode.A))
            {
                directionState = DirectionState.South_West;
            }

            if (currentKeys.Contains(KeyCode.D))
            {
                directionState = DirectionState.South_East;
            }

            else
            {
                directionState = DirectionState.South;
            }
        }

        else if (currentKeys.Contains(KeyCode.A))
        {
            actionState = ActionState.Walking;
            directionState = DirectionState.West;
        }

        else if (currentKeys.Contains(KeyCode.D))
        {
            actionState = ActionState.Walking;
            directionState = DirectionState.East;
        }

        else if (actionState != ActionState.None)
        {
            actionState = ActionState.None;
        }

        if (currentKeys.Contains(KeyCode.LeftShift))
        {
            actionState = ActionState.Dashing;
        }
    }

    private void Movement()
    {
        //ResetAnimatorBools();

        switch (directionState)
        {
            case DirectionState.North:
                Debug.Log("North");
                animator.SetBool("North", true);
                break;

            case DirectionState.North_East:
                animator.SetBool("North_East", true);
                break;

            case DirectionState.East:
                animator.SetBool("East", true);
                break;

            case DirectionState.South_East:
                animator.SetBool("South_East", true);
                break;

            case DirectionState.South:
                animator.SetBool("South", true);
                break;

            case DirectionState.South_West:
                animator.SetBool("South_West", true);
                break;

            case DirectionState.West:
                animator.SetBool("West", true);
                break;

            case DirectionState.North_West:
                animator.SetBool("North_West", true);
                break;
        }

        switch (actionState)
        {
            case ActionState.None:
                
                break;

            case ActionState.Walking:

                break;

            case ActionState.Dashing:

                break;
        }
    }

    private void ResetAnimatorBools()
    {
        animator.SetBool("North", false);
        animator.SetBool("North_East", false);
        animator.SetBool("East", false);
        animator.SetBool("South_East", false);
        animator.SetBool("South", false);
        animator.SetBool("South_West", false);
        animator.SetBool("West", false);
        animator.SetBool("North_West", false);
    }
}