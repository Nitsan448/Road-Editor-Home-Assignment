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
        if (hitGameObject != null && hitGameObject.transform.parent.TryGetComponent(out Junction junction))
        {
            RoadEditor.BuildSectionToJunction(junction);
        }
        else if (hitGameObject != null && hitGameObject.transform.parent.TryGetComponent(out Section section))
        {
            //This breaks save system
            RoadEditor.BuildSectionToSection(section, _mouseRayCastsManager.HitPositionOnTerrain);
        }
        else
        {
            RoadEditor.BuildNewRoad(_mouseRayCastsManager.HitPositionOnTerrain);
        }
    }

    private void TryToSelectJunction()
    {
        GameObject hitGameObject = _mouseRayCastsManager.HitObject;
        if (hitGameObject != null && hitGameObject.transform.parent.TryGetComponent(out Junction junction))
        {
            RoadEditor.SelectJunction(junction);
        }
    }
}
