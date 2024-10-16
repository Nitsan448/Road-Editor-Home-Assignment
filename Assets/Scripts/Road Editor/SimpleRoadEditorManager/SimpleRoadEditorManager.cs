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
    private JunctionsHandler _junctionsHandler;
    private SectionsBuilder _sectionsBuilder;
    private bool _editing = false;

    public override bool Init()
    {
        _junctionsHandler = new JunctionsHandler(_roadNodePrefabsReferencer.JunctionNode);
        _sectionsBuilder = new SectionsBuilder(_roadNodePrefabsReferencer.UnderConstructionNode, _roadNodePrefabsReferencer.BuiltNode);
        _roadValidityCalculator = new RoadValidityCalculator(MaxRoadDistance, MaxHeightDif);
        _roadCostText.Init(_roadValidityCalculator);
        return true;
    }

    public override void StartRoadEdit()
    {
        _editing = true;
        _roadCostText.gameObject.SetActive(true);
        _mouseRayCaster.gameObject.SetActive(true);
        _junctionsHandler.BuildJunction(transform, _firstJunctionPosition);
        _sectionsBuilder.CreateNextSectionPreview();
    }

    private void Update()
    {
        if (!_editing) return;

        UpdateSectionBuilder();
        _roadValidityCalculator.CalculateRoadValidity(_sectionsBuilder.NextSectionStartPoint, _sectionsBuilder.NextSectionEndPoint);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            EditRoads();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //TODO: remove this if statement
            _editing = false;
        }
    }

    private void UpdateSectionBuilder()
    {
        Vector3 startPoint = _junctionsHandler.SelectedJunction.transform.position;
        Vector3 endPoint = _mouseRayCaster.HitPositionOnTerrain;
        _sectionsBuilder.Update(startPoint, endPoint);
    }

    private void EditRoads()
    {
        GameObject hitGameObject = _mouseRayCaster.GetHitObject();
        if (hitGameObject.TryGetComponent(out Terrain terrain))
        {
            if (_roadValidityCalculator.IsRoadPossible)
            {
                BuildRoad();
            }
        }
        else if (hitGameObject.transform.parent.TryGetComponent(out Junction junction))
        {
            SelectJunction(junction);
        }
    }

    public void BuildRoad()
    {
        Vector3 endPoint = _mouseRayCaster.HitPositionOnTerrain;
        _sectionsBuilder.BuildSection();
        _junctionsHandler.BuildJunction(transform, endPoint);
    }

    public void SelectJunction(Junction junction)
    {
        _junctionsHandler.SelectedJunction = junction;
    }

    public void DeleteLastRoad()
    {
        _junctionsHandler.DeleteSelectedJunction();
    }
}
