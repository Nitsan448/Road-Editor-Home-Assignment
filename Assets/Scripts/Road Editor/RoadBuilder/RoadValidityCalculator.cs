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

    //Add max road distance, max height diff to parameters, so we can update the serialized fields
    public void CalculateRoadValidity(Vector3 startPoint, Vector3 endPoint)
    {
        //This is not good, since a height of one does not add to the cost.
        float heightDifference = Mathf.Abs(startPoint.y - endPoint.y);
        heightDifference = Mathf.Max(1, heightDifference);
        float flatDistance = Vector2.Distance(new Vector2(startPoint.x, startPoint.y), new Vector2(endPoint.x, endPoint.y));
        flatDistance = Mathf.Max(1, flatDistance);

        //TODO: split to two methods
        CurrentRoadCost = flatDistance * heightDifference;
        IsRoadPossible = heightDifference < _maxHeightDiff && CurrentRoadCost < _maxRoadDistance;
    }
}
