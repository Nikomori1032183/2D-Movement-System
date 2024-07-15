using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VInspector;

public class UITextbox : UIImage
{
    public UIText textUI;

    [Tab("Textbox")]
    [SerializeField] Dialogue dialogue;
    [SerializeField] bool scroll = true;
    public float scrollInterval = 0.05f;
    [EndTab]

    protected override void Start()
    {
        base.Start();
        textUI = GetComponentInChildren<UIText>();
        UpdateText();
    }

    private void UpdateText()
    {
        textUI.textComponent.text = "";

        if (scroll)
        {
            StartCoroutine(ScrollingText(dialogue.text));
        }

        else
        {
            textUI.textComponent.text = dialogue.text;
        }
    }

    private IEnumerator ScrollingText(string text)
    {
        foreach (char character in text)
        {
            textUI.textComponent.text += character;
            yield return new WaitForSeconds(scrollInterval);
        }
    }

    [Button]
    public void NextDialogue()
    {
        if (dialogue.nextDialogue != null)
        {
            dialogue = dialogue.nextDialogue;
            UpdateText();
        }
    }

    [Button]
    public void PreviousDialogue()
    {
        if (dialogue.previousDialogue != null)
        {
            dialogue = dialogue.previousDialogue;
            UpdateText();
        }
    }
}