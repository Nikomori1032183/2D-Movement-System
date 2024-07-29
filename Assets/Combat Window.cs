using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CombatWindow : UIWindow
{
    UIChoice[] choices;

    protected override void Start()
    {
        choices = GetComponentsInChildren<UIChoice>();
        DisplayMenu(choices[0]);
    }

    public void AttackButton()
    {
        // display attack menu
        DisplayMenu(choices[1]);
    }

    public void DefendButton()
    {
        // defend action

    }

    public void ItemButton()
    {
        // display item menu
        DisplayMenu(choices[2]);
    }

    public void BackButton()
    {

    }

    void DisplayMenu(UIChoice choiceToDisplay)
    {
        foreach (UIChoice choice in choices)
        {
            if (choice != choiceToDisplay)
            {
                choice.gameObject.SetActive(false);
            }

            else
            {
                choice.gameObject.SetActive(true);
            }
        }
    }
}