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
    private RoadEditor _roadEditor;
    private bool _editing = false;


    public override bool Init()
    {
        _roadCostCalculator = new RoadCostCalculator();
        _roadEditor = new RoadEditor(_roadNodePrefabsReferencer, _roadCostCalculator);
        _roadEditUIManager.Init(this, _roadCostCalculator, _mouseRayCastsManager);
        return true;
    }

    public override void StartRoadEdit()
    {
        _editing = true;
        StartedRoadEdit?.Invoke();
        _mouseRayCastsManager.gameObject.SetActive(true);
        _roadEditUIManager.ShowUI();
        _roadEditor.StartBuildingRoads(_firstJunctionPosition);
    }

    private void Update()
    {
        if (!_editing) return;

        _roadEditor.UpdateNextSection(_mouseRayCastsManager.HitPositionOnTerrain);
        _roadCostCalculator.CalculateRoadCost(_roadEditor.NextSectionStartPoint, _roadEditor.NextSectionEndPoint);
        _roadCostCalculator.CalculateRoadValidity(MaxRoadDistance, MaxHeightDif);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            EditRoads();
        }
    }

    private void EditRoads()
    {
        GameObject hitGameObject = _mouseRayCastsManager.HitObject;
        if (hitGameObject != null && hitGameObject.transform.parent.TryGetComponent(out Junction junction))
        {
            _roadEditor.SelectJunction(junction);
            return;
        }

        BuildRoadIfPossible();
    }

    private void BuildRoadIfPossible()
    {
        if (!_roadCostCalculator.IsRoadValid || UIHelpers.IsOverUI()) return;
        _roadEditor.BuildRoad();
    }

    public override void DeleteSelectedRoad()
    {
        _roadEditor.DeleteSelectedRoad();
    }
}
