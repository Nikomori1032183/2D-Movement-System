using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FarmingPrototype : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentToolText;

    public enum Tool
    {
        Hoe, Watering_Can
    }

    public Tool currentTool;

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

    public void Grow()
    {
        foreach (SoilPatch soilPatch in FindObjectsOfType<SoilPatch>())
        {
            soilPatch.Grow();
        }
    }
}
