using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRoadEditorManager : RoadEditorManager_Base
{
    [SerializeField] private RoadNodePrefabsReferencer _roadNodePrefabsReferencer;
    [SerializeField] private Vector3 _firstJunctionPosition = new Vector3(250, 0, -200);
    [SerializeField] private TerrainCollider _terrainCollider;

    private MouseRayCaster _mouseRayCaster;
    private JunctionsHandler _junctionsHandler;
    private SectionsHandler _sectionsHandler;
    private bool _editing = false;

    public override bool Init()
    {
        _mouseRayCaster = new MouseRayCaster(_terrainCollider);
        _junctionsHandler = new JunctionsHandler(_roadNodePrefabsReferencer.JunctionNode);
        _sectionsHandler = new SectionsHandler(_roadNodePrefabsReferencer.UnderConstructionNode, _roadNodePrefabsReferencer.BuiltNode);
        return true;
    }

    public override void StartRoadEdit()
    {
        _editing = true;
        CreateFirstJunction();
    }

    private void CreateFirstJunction()
    {
        _sectionsHandler.CreateSectionPreview();
        _junctionsHandler.BuildJunction(transform, _firstJunctionPosition);
    }


    private void Update()
    {
        if (!_editing) return;

        _mouseRayCaster.Update();
        PreviewSectionBuilding();
        EditRoads();
    }

    private void PreviewSectionBuilding()
    {
        if (_junctionsHandler.SelectedJunction == null) return;
        Vector3 startPoint = _junctionsHandler.SelectedJunction.transform.position;
        Vector3 endPoint = _mouseRayCaster.HitPositionOnTerrain;
        _sectionsHandler.UpdateSectionPreview(startPoint, endPoint);
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
        _sectionsHandler.BuildSection(startPoint, endPoint);
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
