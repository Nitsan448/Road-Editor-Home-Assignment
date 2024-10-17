using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoadCostText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private RoadCostCalculator _roadCostCalculator;

    public void UpdateRoadCostText()
    {
        _text.text = _roadCostCalculator.IsRoadValid ? _roadCostCalculator.CurrentRoadCost.ToString("F0") : "No Access";
    }
}
