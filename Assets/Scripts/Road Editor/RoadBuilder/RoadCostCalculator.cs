using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCostCalculator
{
    public float HeightDifference { get; private set; }
    public float CurrentRoadCost { get; private set; }

    public void CalculateRoadCost(Vector3 startPoint, Vector3 endPoint)
    {
        HeightDifference = CalculateHeightDifference(startPoint.y, endPoint.y);
        float flatDistance = CalculateFlatDistance(new Vector2(startPoint.x, startPoint.z), new Vector2(endPoint.x, endPoint.z));

        CurrentRoadCost = flatDistance * HeightDifference;
    }

    private float CalculateHeightDifference(float startPointHeight, float endPointHeight)
    {
        float heightDifference = Mathf.Abs(startPointHeight - endPointHeight);
        return Mathf.Max(1, heightDifference);
    }

    private float CalculateFlatDistance(Vector2 startPoint, Vector2 endPoint)
    {

        float flatDistance = Vector2.Distance(startPoint, endPoint);
        return Mathf.Max(1, flatDistance);
    }
}
