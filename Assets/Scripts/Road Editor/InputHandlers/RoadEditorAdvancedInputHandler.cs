using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEditorAdvancedInputHandler : ARoadEditorInputHandler
{
    public override void ReactToInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryToEditRoads();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            TryToSelectJunction();
        }
    }

    private void TryToEditRoads()
    {
        if (!_roadCostCalculator.IsRoadValid || UIHelpers.IsOverUI()) return;

        GameObject hitGameObject = _mouseRayCastsManager.HitObject;
        if (hitGameObject == null) return;
        if (hitGameObject.TryGetComponent(out Terrain terrain))
        {
            _roadEditor.BuildNewRoad(_mouseRayCastsManager.HitPositionOnTerrain);
        }
        else if (hitGameObject.HasParent() && hitGameObject.transform.parent.TryGetComponent(out Junction junction))
        {
            _roadEditor.BuildRoadToJunction(junction);
        }
        else if (hitGameObject.HasParent() && hitGameObject.transform.parent.TryGetComponent(out Section section))
        {
            _roadEditor.BuildRoadToSection(section, _mouseRayCastsManager.HitPositionOnTerrain);
        }
    }

    private void TryToSelectJunction()
    {
        GameObject hitGameObject = _mouseRayCastsManager.HitObject;
        if (hitGameObject.HasParent() && hitGameObject.transform.parent.TryGetComponent(out Junction junction))
        {
            _roadEditor.SelectJunction(junction);
        }
    }
}
