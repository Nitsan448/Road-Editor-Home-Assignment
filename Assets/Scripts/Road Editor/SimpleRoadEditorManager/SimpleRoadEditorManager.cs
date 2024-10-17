using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleRoadEditorManager : RoadEditorManager_Base
{
    public Action StartedRoadEdit;

    [SerializeField] private RoadNodePrefabsReferencer _roadNodePrefabsReferencer;
    [SerializeField] private Vector3 _firstJunctionPosition = new Vector3(250, 0, -200);
    [SerializeField] private MouseRayCaster _mouseRayCaster;
    [SerializeField] private RoadCostText _roadCostText;

    private RoadCostCalculator _roadCostCalculator;
    private RoadBuilder _roadBuilder;
    private bool _editing = false;

    public float CurrentRoadCost => _roadCostCalculator.CurrentRoadCost;


    public override bool Init()
    {
        _roadCostCalculator = new RoadCostCalculator();
        _roadBuilder = new RoadBuilder(_roadNodePrefabsReferencer, _roadCostCalculator, _mouseRayCaster);
        return true;
    }

    public override void StartRoadEdit()
    {
        _editing = true;
        StartedRoadEdit?.Invoke();
        _mouseRayCaster.gameObject.SetActive(true);
        _roadBuilder.StartBuildingRoads(_firstJunctionPosition);
    }

    private void Update()
    {
        if (!_editing) return;

        _roadBuilder.UpdateNextSection();
        _roadCostCalculator.CalculateRoadCost(_roadBuilder.NextSectionStartPoint, _roadBuilder.NextSectionEndPoint);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            EditRoads();
        }
    }

    private void EditRoads()
    {
        GameObject hitGameObject = _mouseRayCaster.GetHitObject();
        if (hitGameObject.transform.parent.TryGetComponent(out Junction junction))
        {
            _roadBuilder.SelectJunction(junction);
            return;
        }

        BuildRoadIfPossible();
    }

    private void BuildRoadIfPossible()
    {
        if (!IsRoadValid()) return;
        _roadBuilder.BuildRoad();
    }

    public bool IsRoadValid()
    {
        return _roadCostCalculator.CurrentRoadCost < MaxRoadDistance && _roadCostCalculator.HeightDifference < MaxHeightDif;
    }
}
