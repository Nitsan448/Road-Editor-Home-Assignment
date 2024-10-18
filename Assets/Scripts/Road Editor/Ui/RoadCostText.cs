using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoadCostText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private FixedSizeNearMouseObject _fixedSizeNearMouseObject;
    private RoadCostCalculator _roadCostCalculator;

    public void Init(RoadCostCalculator roadCostCalculator, MouseRayCastsManager mouseRayCastsManager)
    {
        _roadCostCalculator = roadCostCalculator;
        _fixedSizeNearMouseObject.Init(mouseRayCastsManager);
    }

    public void UpdateRoadCostText()
    {
        _text.text = _roadCostCalculator.IsRoadValid ? _roadCostCalculator.CurrentRoadCost.ToString("F0") : "No Access";
    }
}
