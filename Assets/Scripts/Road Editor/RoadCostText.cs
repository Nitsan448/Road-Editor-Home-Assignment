using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoadCostText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;

    public void UpdateRoadCostText(SimpleRoadEditorManager simpleRoadEditorManager)
    {
        _text.text = simpleRoadEditorManager.IsRoadValid() ? simpleRoadEditorManager.CurrentRoadCost.ToString("F0") : "No Access";
    }
}
