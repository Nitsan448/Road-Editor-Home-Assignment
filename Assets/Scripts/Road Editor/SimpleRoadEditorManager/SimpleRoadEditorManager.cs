using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleRoadEditorManager : RoadEditorManager_Base
{
    [SerializeField] private RoadNodePrefabsReferencer _roadNodePrefabsReferencer;
    [SerializeField] private Vector3 _firstJunctionPosition = new Vector3(250, 0, -200);
    [SerializeField] private MouseRayCastsManager _mouseRayCastsManager;
    [SerializeField] private RoadEditUIManager _roadEditUIManager;
    private RoadCostCalculator _roadCostCalculator;
    private RoadBuilder _roadBuilder;
    private bool _editing = false;


    public override bool Init()
    {
        _roadCostCalculator = new RoadCostCalculator();
        _roadBuilder = new RoadBuilder(_roadNodePrefabsReferencer, _roadCostCalculator);
        _roadEditUIManager.Init(this, _roadCostCalculator);
        return true;
    }

    public override void StartRoadEdit()
    {
        _editing = true;
        StartedRoadEdit?.Invoke();
        _mouseRayCastsManager.gameObject.SetActive(true);
        _roadEditUIManager.ShowUI();
        _roadBuilder.StartBuildingRoads(_firstJunctionPosition);
    }

    private void Update()
    {
        if (!_editing) return;

        _roadBuilder.UpdateNextSection(_mouseRayCastsManager.HitPositionOnTerrain);
        _roadCostCalculator.CalculateRoadCost(_roadBuilder.NextSectionStartPoint, _roadBuilder.NextSectionEndPoint);
        _roadCostCalculator.CalculateRoadValidity(MaxRoadDistance, MaxHeightDif);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            EditRoads();
        }
    }

    private void EditRoads()
    {
        GameObject hitGameObject = _mouseRayCastsManager.HitObject;
        if (hitGameObject.transform.TryGetComponent(out Junction junction))
        {
            _roadBuilder.SelectJunction(junction);
            return;
        }

        BuildRoadIfPossible();
    }

    private void BuildRoadIfPossible()
    {
        if (!_roadCostCalculator.IsRoadValid || UIHelpers.IsOverUI()) return;
        _roadBuilder.BuildRoad();
    }

    public override void DeleteSelectedRoad()
    {
        _roadBuilder.DeleteSelectedRoad();
    }
}
