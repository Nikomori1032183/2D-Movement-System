using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using VInspector;

public class UITextButton : UIButton, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    UIText textUI;

    Color textColor;
    
    [Tab("Text")]
    [SerializeField] Color pressedColor;
    [EndTab]

    protected override void Start()
    {
        base.Start();

        textUI = GetComponentInChildren<UIText>();
    }

    protected override void Down()
    {
        base.Down();

        if (textColor == new Color())
        {
            textColor = textUI.textComponent.color;
        }

        textUI.textComponent.color = pressedColor;
    }

    protected override void Up()
    {
        base.Up();

        textUI.textComponent.color = textColor;
    }

    public void SetText(string text)
    {
        textUI.textComponent.text = text;
    }

    public override void Display()
    {
        base.Display();

        textUI.textComponent.enabled = true;
    }

    public override void Hide()
    {
        base.Hide();

        textUI.textComponent.enabled = false;
    }
}