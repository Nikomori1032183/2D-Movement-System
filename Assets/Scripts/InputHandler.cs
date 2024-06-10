using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler current;

    private List<KeyCode> currentKeys = new List<KeyCode>();

    void Awake()
    {
        current = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentKeys.Add(KeyCode.W);
        }

        else if (Input.GetKeyUp(KeyCode.W))
        {
            currentKeys.Remove(KeyCode.W);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentKeys.Add(KeyCode.A);
        }

        else if (Input.GetKeyUp(KeyCode.A))
        {
            currentKeys.Remove(KeyCode.A);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            currentKeys.Add(KeyCode.S);
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            currentKeys.Remove(KeyCode.S);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            currentKeys.Add(KeyCode.D);
        }

        else if (Input.GetKeyUp(KeyCode.D))
        {
            currentKeys.Remove(KeyCode.D);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentKeys.Add(KeyCode.LeftShift);
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentKeys.Remove(KeyCode.LeftShift);
        }
    }

    public List<KeyCode> GetCurrentKeys()
    {
        return currentKeys;
    }
}
