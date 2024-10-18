using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEditorManager : RoadEditorManager_Base
{
    public Action StartedRoadEdit;
    public RoadCostCalculator RoadCostCalculator { get; private set; }

    [SerializeField] private RoadNodePrefabsReferencer _roadNodePrefabsReferencer;
    [SerializeField] private Vector3 _firstJunctionPosition = new Vector3(250, 0, -200);
    [SerializeField] private MouseRayCastsManager _mouseRayCastsManager;
    [SerializeField] private RoadEditUIManager _roadEditUIManager;
    [SerializeField] private ARoadEditorInputHandler _roadEditorInputHandler;
    [SerializeField] private Transform _builtRoadsParent;
    private RoadEditor _roadEditor;
    private bool _editing = false;


    public override bool Init()
    {
        RoadCostCalculator = new RoadCostCalculator();
        _roadEditor = new RoadEditor(_roadNodePrefabsReferencer, _builtRoadsParent);
        _roadEditUIManager.Init(this, RoadCostCalculator, _mouseRayCastsManager);
        _roadEditorInputHandler.Init(_mouseRayCastsManager, _roadEditor, RoadCostCalculator);
        return true;
    }

    public override void StartRoadEdit()
    {
        _editing = true;
        StartedRoadEdit?.Invoke();
        gameObject.SetActive(true);
        _roadEditor.StartBuildingRoads(_firstJunctionPosition);
    }

    private void Update()
    {
        if (!_editing) return;

        _roadEditor.UpdateNextSectionPreview(_mouseRayCastsManager.HitPositionOnTerrain);
        RoadCostCalculator.CalculateRoadCost(_roadEditor.SelectedJunctionPosition, _mouseRayCastsManager.HitPositionOnTerrain);
        RoadCostCalculator.CalculateRoadValidity(MaxRoadDistance, MaxHeightDif);
        _roadEditorInputHandler.ReactToInput();
    }

    public void DeleteSelectedRoad()
    {
        _roadEditor.DeleteSelectedRoad();
    }
}
