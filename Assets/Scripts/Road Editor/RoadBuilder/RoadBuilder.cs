using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class RoadBuilder
{
    private JunctionsHandler _junctionsHandler;
    private SectionsHandler _sectionsHandler;
    private RoadBuilderDataPersistence _dataPersistence;
    private RoadCostCalculator _roadCostCalculator;

    public Vector3 NextSectionStartPoint;
    public Vector3 NextSectionEndPoint;

    public RoadBuilder(RoadNodePrefabsReferencer roadNodePrefabsReferencer, RoadCostCalculator roadCostCalculator)
    {
        _roadCostCalculator = roadCostCalculator;
        _junctionsHandler = new JunctionsHandler(roadNodePrefabsReferencer.JunctionNode);
        _sectionsHandler = new SectionsHandler(roadNodePrefabsReferencer.UnderConstructionNode, roadNodePrefabsReferencer.BuiltNode);
        _dataPersistence = new RoadBuilderDataPersistence(_junctionsHandler, _sectionsHandler);
    }

    public void StartBuildingRoads(Vector3 firstJunctionPosition)
    {
        _junctionsHandler.BuildJunction(firstJunctionPosition);
        _sectionsHandler.CreateNextSectionPreview();
    }

    public void UpdateNextSection(Vector3 hitPositionOnTerrain)
    {
        NextSectionStartPoint = _junctionsHandler.SelectedJunction.transform.position;
        NextSectionEndPoint = hitPositionOnTerrain;
        _sectionsHandler.UpdateNextSectionPoints(NextSectionStartPoint, NextSectionEndPoint);
        _sectionsHandler.UpdateNextSectionPreview();
    }

    public void BuildRoad()
    {
        Section builtSection = _sectionsHandler.BuildSection();
        builtSection.StartJunction = _junctionsHandler.SelectedJunction;
        _junctionsHandler.SelectedJunction.ConnectedSections.Add(builtSection);

        Junction builtJunction = _junctionsHandler.BuildJunction(NextSectionEndPoint);
        builtSection.EndJunction = builtJunction;
        builtJunction.ConnectedSections.Add(builtSection);
    }

    public void SelectJunction(Junction junction)
    {
        _junctionsHandler.SelectedJunction = junction;
    }

    public void DeleteSelectedRoad()
    {
        DeleteRoad(_junctionsHandler.SelectedJunction);
    }

    private void DeleteRoad(Junction junctionToDelete)
    {
        List<Junction> connectedJunctions = junctionToDelete.GetConnectedJunctions();
        if (connectedJunctions.Count == 0) return;

        _junctionsHandler.SelectedJunction = connectedJunctions[0];
        DeleteConnectedSections(junctionToDelete);
        DeleteEmptyJunctions(connectedJunctions);
        _junctionsHandler.DeleteJunction(junctionToDelete);
    }

    private void DeleteConnectedSections(Junction junction)
    {
        foreach (Section section in junction.ConnectedSections)
        {
            _sectionsHandler.DeleteSection(section);
        }
    }

    private void DeleteEmptyJunctions(List<Junction> junctions)
    {
        foreach (Junction junction in junctions)
        {
            if (junction.ConnectedSections.Count == 0)
            {
                _junctionsHandler.DeleteJunction(junction);
            }
        }
    }
}
