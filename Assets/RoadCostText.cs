using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoadCostText : MonoBehaviour
{
    [SerializeField] private SimpleRoadEditorManager _simpleRoadEditorManager;
    [SerializeField] private FixedSizeWorldUI _textWorldUI;
    [SerializeField] private TextMeshPro _text;

    private void Update()
    {
        UpdateRoadCostText();
        _textWorldUI.UpdatePosition(_simpleRoadEditorManager.MouseRayCaster.HitPositionOnTerrain);
    }

    private void UpdateRoadCostText()
    {
        bool isRoadPossible = _simpleRoadEditorManager.RoadCostCalculator.IsRoadPossible;
        float currentRoadCost = _simpleRoadEditorManager.RoadCostCalculator.CurrentRoadCost;
        _text.text = isRoadPossible ? currentRoadCost.ToString("F0") : "No Access";
    }

}
