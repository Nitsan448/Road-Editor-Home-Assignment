using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRoadEditorManager : RoadEditorManager_Base
{
    [SerializeField] private RoadNodePrefabsReferencer _roadNodePrefabsReferencer;
    [SerializeField] private Vector3 _firstJunctionPosition = new Vector3(250, 0, -200);
    [SerializeField] private TerrainCollider _terrainCollider;

    private MouseRayCaster _mouseRayCaster;
    private JunctionsHandler _junctionsHandler;
    private SectionsHandler _sectionsHandler;
    private SelectJunctionOrBuildRoadCommand _selectJunctionOrBuildRoadCommand;
    private bool _editing = false;

    public override bool Init()
    {
        _mouseRayCaster = new MouseRayCaster(_terrainCollider);
        _junctionsHandler = new JunctionsHandler(_roadNodePrefabsReferencer.JunctionNode);
        _sectionsHandler = new SectionsHandler(_roadNodePrefabsReferencer.UnderConstructionNode, _roadNodePrefabsReferencer.BuiltNode);
        _selectJunctionOrBuildRoadCommand = new SelectJunctionOrBuildRoadCommand(_mouseRayCaster, this);
        return true;
    }

    public override void StartRoadEdit()
    {
        Debug.Log("Starting road edit");
        CreateFirstJunction();
        _editing = true;
    }

    private void CreateFirstJunction()
    {
        _sectionsHandler.CreateSectionPreview();
        _junctionsHandler.BuildJunction(transform, _firstJunctionPosition);
    }


    private void Update()
    {
        if (_editing)
        {
            _mouseRayCaster.Update();
            PreviewSectionBuilding();
            EditRoads();
        }
    }

    private void PreviewSectionBuilding()
    {
        if (_junctionsHandler.SelectedJunction == null) return;
        Vector3 startPoint = _junctionsHandler.SelectedJunction.transform.position;
        Vector3 endPoint = _mouseRayCaster.HitPositionOnTerrain;
        _sectionsHandler.UpdateSectionPreview(startPoint, endPoint);
    }

    private void EditRoads()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _selectJunctionOrBuildRoadCommand.Execute();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _selectJunctionOrBuildRoadCommand.Undo();
        }
    }

    public void BuildRoad()
    {
        Junction selectedJunction = _junctionsHandler.SelectedJunction;
        Vector3 startPoint = selectedJunction.transform.position;
        Vector3 endPoint = _mouseRayCaster.HitPositionOnTerrain;
        _sectionsHandler.BuildSection(startPoint, endPoint);
        _junctionsHandler.BuildJunction(transform, endPoint);
    }

    public void SelectJunction(Junction junction)
    {
        Debug.Log("here");
        _junctionsHandler.SelectedJunction = junction;
    }

    public void DeleteLastRoad()
    {
        _junctionsHandler.DeleteLastJunction();
    }
}
