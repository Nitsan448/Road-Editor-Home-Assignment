using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCostCalculator
{
    public float CurrentRoadCost { get; private set; }

    public void CalculateCost(Vector3 startPoint, Vector3 endPoint)
    {
        CurrentRoadCost = Vector3.Distance(startPoint, endPoint);
    }
}
