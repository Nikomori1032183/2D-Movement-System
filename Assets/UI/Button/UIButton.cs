using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VInspector;

public class UIButton : UIImage, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Sprite sprite;

    [Tab("Button")]
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private PointerEventData.InputButton mouseButton;
    [SerializeField] private UnityEvent buttonPress;
    [EndTab]

    protected override void Start()
    {
        base.Start();
        sprite = image.sprite;
    }

    private void ButtonPress()
    {
        buttonPress.Invoke();
    }

    private bool CorrectButton(PointerEventData.InputButton button)
    {
        return button == mouseButton;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CorrectButton(eventData.button))
        {
            Down();
        }
    }

    protected virtual void Down()
    {
        DebugMessage("Button Down");
        image.sprite = pressedSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CorrectButton(eventData.button))
        {
            Click();
        }
    }

    protected virtual void Click()
    {
        DebugMessage("Button Click");
        ButtonPress();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (CorrectButton(eventData.button))
        {
            Up();
        }
    }

    protected virtual void Up()
    {
        DebugMessage("Button Up");
        image.sprite = sprite;
    }
}