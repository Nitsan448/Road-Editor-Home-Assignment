using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadValidityCalculator
{
    private float _maxRoadDistance;
    private float _maxHeightDiff;
    public float CurrentRoadCost { get; private set; }
    public bool IsRoadPossible { get; private set; }

    public RoadValidityCalculator(float maxRoadDistance, float maxHeightDiff)
    {
        _maxRoadDistance = maxRoadDistance;
        _maxHeightDiff = maxHeightDiff;
    }

    public void CalculateRoadValidity(Vector3 startPoint, Vector3 endPoint)
    {
        float heightDifference = Mathf.Abs(startPoint.y - endPoint.y);
        heightDifference = Mathf.Max(1, heightDifference);
        float flatDistance = Vector2.Distance(new Vector2(startPoint.x, startPoint.y), new Vector2(endPoint.x, endPoint.y));
        CurrentRoadCost = flatDistance * heightDifference;
        IsRoadPossible = heightDifference < _maxHeightDiff && CurrentRoadCost < _maxRoadDistance;
    }
}
