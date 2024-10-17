using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilder
{
    private JunctionsHandler _junctionsHandler;
    private SectionsBuilder _sectionsBuilder;
    private RoadBuilderDataPersistence _dataPersistence;
    private RoadValidityCalculator _roadValidityCalculator;
    private MouseRayCaster _mouseRayCaster;

    public Vector3 NextSectionStartPoint;
    public Vector3 NextSectionEndPoint;

    public RoadBuilder(RoadNodePrefabsReferencer roadNodePrefabsReferencer, RoadValidityCalculator roadValidityCalculator,
        MouseRayCaster mouseRayCaster)
    {
        _roadValidityCalculator = roadValidityCalculator;
        _mouseRayCaster = mouseRayCaster;
        _junctionsHandler = new JunctionsHandler(roadNodePrefabsReferencer.JunctionNode);
        _sectionsBuilder = new SectionsBuilder(roadNodePrefabsReferencer.UnderConstructionNode, roadNodePrefabsReferencer.BuiltNode);
        _dataPersistence = new RoadBuilderDataPersistence(_junctionsHandler, _sectionsBuilder);
    }

    public void StartBuildingRoads(Vector3 firstJunctionPosition)
    {
        _junctionsHandler.BuildJunction(firstJunctionPosition);
        _sectionsBuilder.CreateNextSectionPreview();
    }

    public void Update()
    {
        NextSectionStartPoint = _junctionsHandler.SelectedJunction.transform.position;
        NextSectionEndPoint = _mouseRayCaster.HitPositionOnTerrain;
        _sectionsBuilder.UpdateNextSectionPoints(NextSectionStartPoint, NextSectionEndPoint);
        _sectionsBuilder.UpdateNextSectionPreview();
    }

    public void BuildRoad()
    {
        Section builtSection = _sectionsBuilder.BuildSection();
        builtSection.StartJunction = _junctionsHandler.SelectedJunction;
        _junctionsHandler.SelectedJunction.ConnectedSections.Add(builtSection);
        Junction builtJunction = _junctionsHandler.BuildJunction(NextSectionEndPoint);
        builtSection.EndJunction = builtJunction;
    }

    public void SelectJunction(Junction junction)
    {
        _junctionsHandler.SelectedJunction = junction;
    }
}
