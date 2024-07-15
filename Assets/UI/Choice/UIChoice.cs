using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

public class UIChoice : UIElement
{
    [Tab("UIChoice")]
    public List<UIButton> buttons = new List<UIButton>();

    [SerializeField] bool destroy = false;

    protected override void Start()
    {
        base.Start();

        foreach (UIButton button in GetComponentsInChildren<UIButton>())
        {
            buttons.Add(button);
        }
    }

    public void ChoicePicked()
    {
        DebugMessage("Choice Picked");

        if (destroy)
        {
            Destroy(gameObject);
        }

        else
        {
            gameObject.SetActive(false);
        }
    }

    public override void Hide()
    {
        base.Hide();

        foreach (UIButton button in buttons)
        {
            button.Hide();
        }
    }

    public override void Display()
    {
        base.Display();

        foreach (UIButton button in buttons)
        {
            button.Display();
        }
    }
}
