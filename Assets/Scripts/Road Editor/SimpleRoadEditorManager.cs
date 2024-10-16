using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRoadEditorManager : RoadEditorManager_Base
{
    private MouseRayCaster _mouseRayCaster;
    private JunctionsHandler _junctionsHandler;
    private SectionBuilder _sectionBuilder;
    private SelectJunctionOrBuildRoadCommand _selectJunctionOrBuildRoadCommand;
    private bool _editing = false;


    public override bool Init()
    {
        _mouseRayCaster = new MouseRayCaster();
        _junctionsHandler = new JunctionsHandler();
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
            EditRoads();
        }
    }

    private void EditRoads()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _selectJunctionOrBuildRoadCommand.Execute();
        }
    }


    public void BuildRoad()
    {
        Vector3 startPoint = _junctionsHandler.SelectedJunction.transform.position;
        Vector3 endPoint = _mouseRayCaster.GetMousePosition();
        Debug.Log(endPoint);
        _sectionBuilder.BuildSection(startPoint, endPoint);
    }

    public void SelectJunction(Junction junction)
    {

    }
}
