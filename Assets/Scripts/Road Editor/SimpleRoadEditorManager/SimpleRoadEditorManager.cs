using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleRoadEditorManager : RoadEditorManager_Base
{
    [SerializeField] private RoadNodePrefabsReferencer _roadNodePrefabsReferencer;
    [SerializeField] private Vector3 _firstJunctionPosition = new Vector3(250, 0, -200);
    [SerializeField] private TerrainCollider _terrainCollider;
    [SerializeField] private DistanceText _distanceText;

    private MouseRayCaster _mouseRayCaster;
    private JunctionsHandler _junctionsHandler;
    private SectionsBuilder _sectionsBuilder;
    private bool _editing = false;


    public override bool Init()
    {
        _mouseRayCaster = new MouseRayCaster(_terrainCollider);
        _junctionsHandler = new JunctionsHandler(_roadNodePrefabsReferencer.JunctionNode);
        _sectionsBuilder = new SectionsBuilder(_roadNodePrefabsReferencer.UnderConstructionNode, _roadNodePrefabsReferencer.BuiltNode);
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

        _mouseRayCaster.Update();
        PreviewNextSection();
        UpdateDistanceText();
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

    private void PreviewNextSection()
    {
        Vector3 startPoint = _junctionsHandler.SelectedJunction.transform.position;
        Vector3 endPoint = _mouseRayCaster.HitPositionOnTerrain;
        _sectionsBuilder.UpdateNextSectionPreview(startPoint, endPoint);
    }

    private void UpdateDistanceText()
    {
        _distanceText.UpdateText("No Access 1");
        _distanceText.UpdatePosition(_mouseRayCaster.HitPositionOnTerrain);
    }

    private void EditRoads()
    {
        GameObject hitGameObject = _mouseRayCaster.GetHitObject();
        if (hitGameObject.TryGetComponent(out Terrain terrain))
        {
            BuildRoad();
        }
        else if (hitGameObject.transform.parent.TryGetComponent(out Junction junction))
        {
            SelectJunction(junction);
        }
    }

    public void BuildRoad()
    {
        Vector3 startPoint = _junctionsHandler.SelectedJunction.transform.position;
        Vector3 endPoint = _mouseRayCaster.HitPositionOnTerrain;
        _sectionsBuilder.BuildSection(startPoint, endPoint);
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
