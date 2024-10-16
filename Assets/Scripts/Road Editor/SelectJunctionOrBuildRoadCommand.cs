using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectJunctionOrBuildRoadCommand : ICommand
{
    private MouseRayCaster _mouseRayCaster;
    private SimpleRoadEditorManager _simpleRoadEditorManager;

    public SelectJunctionOrBuildRoadCommand(MouseRayCaster mouseRayCaster, SimpleRoadEditorManager simpleRoadEditorManager)
    {
        _mouseRayCaster = mouseRayCaster;
        _simpleRoadEditorManager = simpleRoadEditorManager;
    }

    public void Execute()
    {
        GameObject hitGameObject = _mouseRayCaster.GetHitObject();
        if (hitGameObject.TryGetComponent(out Terrain terrain))
        {
            _simpleRoadEditorManager.BuildRoad();
        }
        else if (hitGameObject.TryGetComponent(out Junction junction))
        {
            _simpleRoadEditorManager.SelectJunction(junction);
        }
    }

    public void Undo()
    {
        _simpleRoadEditorManager.DeleteLastRoad();
    }
}
