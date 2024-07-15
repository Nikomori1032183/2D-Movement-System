using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

public abstract class UIElement : DebugToggleComponent
{
    public static event Action onUIDisplay;
    public static event Action onUIHide;
    public static event Action onUIDestroy;

    protected virtual void Start()
    {

    }

    public virtual void Display()
    {
        if (onUIDisplay != null)
        {
            onUIDisplay();
        }

        DebugMessage("Display");
    }

    public virtual void Hide()
    {
        if (onUIHide != null)
        {
            onUIHide();
        }

        DebugMessage("Hide");
    }

    public void Destroy()
    {
        if (onUIDestroy != null)
        {
            onUIDestroy();
        }

        DebugMessage("Destroy");

        Destroy(gameObject);
    }
}
