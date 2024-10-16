using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleRoadEditorManager : RoadEditorManager_Base
{
    public RoadCostCalculator RoadCostCalculator { get; private set; }
    public MouseRayCaster MouseRayCaster { get; private set; }

    [SerializeField] private RoadNodePrefabsReferencer _roadNodePrefabsReferencer;
    [SerializeField] private Vector3 _firstJunctionPosition = new Vector3(250, 0, -200);
    [SerializeField] private TerrainCollider _terrainCollider;
    [SerializeField] private FixedSizeWorldUI _fixedSizeWorldUI;

    private JunctionsHandler _junctionsHandler;
    private SectionsBuilder _sectionsBuilder;
    private bool _editing = false;


    public override bool Init()
    {
        MouseRayCaster = new MouseRayCaster(_terrainCollider);
        _junctionsHandler = new JunctionsHandler(_roadNodePrefabsReferencer.JunctionNode);
        _sectionsBuilder = new SectionsBuilder(_roadNodePrefabsReferencer.UnderConstructionNode, _roadNodePrefabsReferencer.BuiltNode);
        RoadCostCalculator = new RoadCostCalculator(MaxRoadDistance, MaxHeightDif);
        return true;
    }

    public override void StartRoadEdit()
    {
        _editing = true;
        _junctionsHandler.BuildJunction(transform, _firstJunctionPosition);
        _sectionsBuilder.CreateNextSectionPreview();
    }

    private void Update()
    {
        if (!_editing) return;

        MouseRayCaster.Update();
        UpdateSectionBuilder();
        RoadCostCalculator.CalculateCost(_sectionsBuilder.NextSectionStartPoint, _sectionsBuilder.NextSectionEndPoint);
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
        Vector3 endPoint = MouseRayCaster.HitPositionOnTerrain;
        _sectionsBuilder.Update(startPoint, endPoint);
    }

    private void EditRoads()
    {
        GameObject hitGameObject = MouseRayCaster.GetHitObject();
        if (hitGameObject.TryGetComponent(out Terrain terrain))
        {
            if (RoadCostCalculator.IsRoadPossible)
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
        Vector3 endPoint = MouseRayCaster.HitPositionOnTerrain;
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
