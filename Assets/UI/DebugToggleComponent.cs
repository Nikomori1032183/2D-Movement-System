using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class DebugToggleComponent : MonoBehaviour
{
    [Tab("Debug")]
    [SerializeField] private bool debug = true;
    [EndTab]
    protected void DebugMessage(string message)
    {
        if (debug)
        {
            Debug.Log(name + " - " + message);
        }
    }
}
