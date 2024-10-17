using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEditorManager : RoadEditorManager_Base
{
    public Action StartedRoadEdit;
    [SerializeField] private RoadNodePrefabsReferencer _roadNodePrefabsReferencer;
    [SerializeField] private Vector3 _firstJunctionPosition = new Vector3(250, 0, -200);
    [SerializeField] private MouseRayCastsManager _mouseRayCastsManager;
    [SerializeField] private RoadEditUIManager _roadEditUIManager;
    [SerializeField] private ARoadEditorInputHandler _roadEditorInputHandler;
    private RoadCostCalculator _roadCostCalculator;
    private RoadEditor _roadEditor;
    private bool _editing = false;


    public override bool Init()
    {
        _roadCostCalculator = new RoadCostCalculator();
        _roadEditor = new RoadEditor(_roadNodePrefabsReferencer);
        _roadEditUIManager.Init(this, _roadCostCalculator, _mouseRayCastsManager);
        _roadEditorInputHandler.Init(_mouseRayCastsManager, _roadEditor, _roadCostCalculator);
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
        _roadEditorInputHandler.ReactToInput();
    }

    public void DeleteSelectedRoad()
    {
        _roadEditor.DeleteSelectedRoad();
    }
}
