using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEditorAdvancedInputHandler : ARoadEditorManagerInputHandler
{
    public override void ReactToInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            TryToSelectJunction();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            BuildRoadIfPossible();
        }
    }

    private void TryToSelectJunction()
    {
        GameObject hitGameObject = _mouseRayCastsManager.HitObject;
        if (hitGameObject != null && hitGameObject.transform.parent.TryGetComponent(out Junction junction))
        {
            _roadEditor.SelectJunction(junction);
        }
    }

    private void BuildRoadIfPossible()
    {
        if (!_roadCostCalculator.IsRoadValid || UIHelpers.IsOverUI()) return;
        GameObject hitGameObject = _mouseRayCastsManager.HitObject;
        if (hitGameObject != null && hitGameObject.transform.parent.TryGetComponent(out Junction junction))
        {
            _roadEditor.BuildSectionToJunction(junction);
        }
        else if (hitGameObject != null && hitGameObject.transform.parent.TryGetComponent(out Section section))
        {
            _roadEditor.SplitSection(section, _mouseRayCastsManager.HitPositionOnTerrain);
        }
        else
        {
            _roadEditor.BuildNewRoad();
        }
    }
}
