using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleRoadEditorManager : RoadEditorManager_Base
{
    [SerializeField] private RoadNodePrefabsReferencer _roadNodePrefabsReferencer;
    [SerializeField] private Vector3 _firstJunctionPosition = new Vector3(250, 0, -200);
    [SerializeField] private MouseRayCaster _mouseRayCaster;
    [SerializeField] private RoadCostText _roadCostText;

    private RoadValidityCalculator _roadValidityCalculator;
    private RoadBuilder _roadBuilder;
    private bool _editing = false;


    public override bool Init()
    {
        _roadValidityCalculator = new RoadValidityCalculator(MaxRoadDistance, MaxHeightDif);
        _roadBuilder = new RoadBuilder(_roadNodePrefabsReferencer, _roadValidityCalculator, _mouseRayCaster);
        _roadCostText.Init(_roadValidityCalculator);
        return true;
    }

    public override void StartRoadEdit()
    {
        _editing = true;
        //Consider having a child game object that holds those that is set active, instead of each one
        _roadCostText.gameObject.SetActive(true);
        _mouseRayCaster.gameObject.SetActive(true);
        _roadBuilder.StartBuildingRoads(_firstJunctionPosition);
    }

    private void Update()
    {
        if (!_editing) return;

        _roadBuilder.Update();
        _roadValidityCalculator.CalculateRoadValidity(_roadBuilder.NextSectionStartPoint, _roadBuilder.NextSectionEndPoint);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            EditRoads();
        }
    }

    private void EditRoads()
    {
        GameObject hitGameObject = _mouseRayCaster.GetHitObject();
        if (hitGameObject.TryGetComponent(out Terrain terrain))
        {
            BuildRoadIfPossible();
        }
        else if (hitGameObject.transform.parent.TryGetComponent(out Junction junction))
        {
            _roadBuilder.SelectJunction(junction);
        }
    }

    private void BuildRoadIfPossible()
    {
        if (!_roadValidityCalculator.IsRoadPossible) return;
        _roadBuilder.BuildRoad();
    }
}
