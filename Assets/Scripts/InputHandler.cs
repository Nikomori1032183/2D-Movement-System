using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler current;

    private List<KeyCode> heldKeys = new List<KeyCode>();

    [SerializeField] private bool debugging;


    void Awake()
    {
        current = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            heldKeys.Add(KeyCode.W);
            PressKey(KeyCode.W);
        }

        else if (Input.GetKeyUp(KeyCode.W))
        {
            heldKeys.Remove(KeyCode.W);
            ReleaseKey(KeyCode.W);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            heldKeys.Add(KeyCode.A);
            PressKey(KeyCode.A);
        }

        else if (Input.GetKeyUp(KeyCode.A))
        {
            heldKeys.Remove(KeyCode.A);
            ReleaseKey(KeyCode.A);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            heldKeys.Add(KeyCode.S);
            PressKey(KeyCode.S);
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            heldKeys.Remove(KeyCode.S);
            ReleaseKey(KeyCode.S);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            heldKeys.Add(KeyCode.D);
            PressKey(KeyCode.D);
        }

        else if (Input.GetKeyUp(KeyCode.D))
        {
            heldKeys.Remove(KeyCode.D);
            ReleaseKey(KeyCode.D);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            heldKeys.Add(KeyCode.LeftShift);
            PressKey(KeyCode.LeftShift);
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            heldKeys.Remove(KeyCode.LeftShift);
            ReleaseKey(KeyCode.LeftShift);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Debug.Log(GetMouseGridPosition());
        }
    }

    public Vector2 GetMovement()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public List<KeyCode> GetHeldKeys()
    {
        return heldKeys;
    }

    public bool IsKeyHeld(KeyCode keyCode)
    {
        if (heldKeys.Contains(keyCode))
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public event Action<KeyCode> onPressKey;
    public void PressKey(KeyCode keyCode)
    {
        if (debugging)
        {
            Debug.Log("PressKey " + keyCode.ToString());
        }

        if (onPressKey != null)
        {
            onPressKey(keyCode);
        }
    }

    public event Action<KeyCode> onReleaseKey;
    public void ReleaseKey(KeyCode keyCode)
    {
        if (debugging)
        {
            Debug.Log("ReleaseKey " + keyCode.ToString());
        }

        if (onReleaseKey != null)
        {
            onReleaseKey(keyCode);
        }
    }

    // Currently with the way this works anything that depends on a input would be told whenever any of the inputs are pressed, even if its not the desired inputs, if one wanted to make that not happen for efficiency
    // they could create seperate events for every key.

    public Vector2 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector3Int GetMouseGridPosition()
    {
        return GridInfo.current.grid.WorldToCell(GetMouseWorldPosition());
    }
}
