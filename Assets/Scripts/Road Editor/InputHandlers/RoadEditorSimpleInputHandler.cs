using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEditorSimpleInputHandler : ARoadEditorInputHandler
{
    public override void ReactToInput()
    {
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
        _roadEditor.BuildNewRoad(_mouseRayCastsManager.HitPositionOnTerrain);
    }
}
