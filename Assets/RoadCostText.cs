using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoadCostText : MonoBehaviour
{
    [SerializeField] private SimpleRoadEditorManager _simpleRoadEditorManager;
    [SerializeField] private TextMeshPro _text;

    private void Update()
    {
        UpdateRoadCostText();
    }

    private void UpdateRoadCostText()
    {
        bool isRoadPossible = _simpleRoadEditorManager.RoadValidityCalculator.IsRoadPossible;
        float currentRoadCost = _simpleRoadEditorManager.RoadValidityCalculator.CurrentRoadCost;
        _text.text = isRoadPossible ? currentRoadCost.ToString("F0") : "No Access";
    }
}
