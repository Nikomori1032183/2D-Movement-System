using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FarmingPrototype : MonoBehaviour
{
    public static FarmingPrototype current;

    [SerializeField] private TextMeshProUGUI currentToolText;

    public enum Tool
    {
        Hoe, Watering_Can, Plant_Seeds
    }

    public Tool currentTool;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        UpdateCurrentTool();
    }

    private void UpdateCurrentTool()
    {
        currentToolText.text = "Current Tool: \n" + currentTool.ToString();
    }

    public void Hoe()
    {
        currentTool = Tool.Hoe;
        UpdateCurrentTool();
    }

    public void WateringCan()
    {
        currentTool = Tool.Watering_Can;
        UpdateCurrentTool();
    }

    public void Plant()
    {
        currentTool = Tool.Plant_Seeds;
        UpdateCurrentTool();
    }

    public void Grow()
    {
        foreach (SoilPatch soilPatch in FindObjectsOfType<SoilPatch>())
        {
            soilPatch.Grow();
        }
    }
}