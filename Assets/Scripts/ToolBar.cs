using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VInspector.Libs;

public class ToolBar : MonoBehaviour
{
    private List<Button> buttons = new List<Button>();
    [SerializeField] Color pressedColor;

    void Start()
    {
        buttons = GetComponentsInChildren<Button>().ToList();
    }

    public void ToolButtonPressed(Button pressedButton)
    {
        pressedButton.image.color = pressedColor;
        foreach (Button button in buttons)
        {
            if (button != pressedButton)
            {
                button.image.color = Color.white;
            }
        }
    }
}
