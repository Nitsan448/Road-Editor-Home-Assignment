using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRoadEditorManager : RoadEditorManager_Base
{
    [SerializeField] private RoadNodePrefabsReferencer _roadNodePrefabsReferencer;
    [SerializeField] private Vector3 _firstJunctionPosition = new Vector3(250, 0, -200);

    private MouseRayCaster _mouseRayCaster;
    private JunctionsHandler _junctionsHandler;
    private SectionHandler _sectionHandler;
    private SelectJunctionOrBuildRoadCommand _selectJunctionOrBuildRoadCommand;
    private bool _editing = false;

    public override bool Init()
    {
        _mouseRayCaster = new MouseRayCaster();
        _junctionsHandler = new JunctionsHandler(_roadNodePrefabsReferencer.JunctionNode);
        _sectionHandler = new SectionHandler(_roadNodePrefabsReferencer.UnderConstructionNode, _roadNodePrefabsReferencer.BuiltNode);
        _selectJunctionOrBuildRoadCommand = new SelectJunctionOrBuildRoadCommand(_mouseRayCaster, this);
        return true;
    }

    public override void StartRoadEdit()
    {
        Debug.Log("Starting road edit");
        _editing = true;
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
        if (_junctionsHandler.GetSelectedJunction() == null) return;
        Vector3 startPoint = _junctionsHandler.GetSelectedJunction().transform.position;
        Vector3 endPoint = _mouseRayCaster.HitPosition;
        _sectionHandler.ShowSectionPreview(startPoint, endPoint);
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
        Junction selectedJunction = _junctionsHandler.GetSelectedJunction();
        Vector3 startPoint = selectedJunction == null ? _firstJunctionPosition : selectedJunction.transform.position;
        Vector3 endPoint = selectedJunction == null ? _firstJunctionPosition : _mouseRayCaster.HitPosition;
        _sectionHandler.BuildSection(startPoint, endPoint);
        _junctionsHandler.BuildJunction(transform, endPoint);
    }

    public void SelectJunction(Junction junction)
    {

    }

    public void DeleteLastRoad()
    {
        _junctionsHandler.DeleteLastJunction();
    }
}
