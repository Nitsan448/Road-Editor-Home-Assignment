using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoadCostText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    private RoadValidityCalculator _roadValidityCalculator;

    public void Init(RoadValidityCalculator roadValidityCalculator)
    {
        _roadValidityCalculator = roadValidityCalculator;
    }

    private void Update()
    {
        UpdateRoadCostText();
    }

    private void UpdateRoadCostText()
    {
        _text.text = _roadValidityCalculator.IsRoadPossible ? _roadValidityCalculator.CurrentRoadCost.ToString("F0") : "No Access";
    }
}
