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
    private SectionBuilder _sectionBuilder;
    private SelectJunctionOrBuildRoadCommand _selectJunctionOrBuildRoadCommand;
    private bool _editing = false;

    public override bool Init()
    {
        _mouseRayCaster = new MouseRayCaster();
        _junctionsHandler = new JunctionsHandler(_roadNodePrefabsReferencer.JunctionNode);
        _sectionBuilder = new SectionBuilder();
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
            EditRoads();
        }
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
        Debug.Log(startPoint);
        Debug.Log(endPoint);
        _sectionBuilder.BuildSection(startPoint, endPoint);
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
