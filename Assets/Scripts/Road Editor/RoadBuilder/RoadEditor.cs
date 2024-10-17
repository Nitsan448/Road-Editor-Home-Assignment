using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class RoadEditor
{
    private JunctionsEditor _junctionsEditor;
    private SectionsEditor _sectionsEditor;
    private RoadBuilderDataPersistence _dataPersistence;
    private RoadCostCalculator _roadCostCalculator;

    public Vector3 NextSectionStartPoint;
    public Vector3 NextSectionEndPoint;

    public RoadEditor(RoadNodePrefabsReferencer roadNodePrefabsReferencer, RoadCostCalculator roadCostCalculator)
    {
        _roadCostCalculator = roadCostCalculator;
        _junctionsEditor = new JunctionsEditor(roadNodePrefabsReferencer.JunctionNode);
        _sectionsEditor = new SectionsEditor(roadNodePrefabsReferencer.UnderConstructionNode, roadNodePrefabsReferencer.BuiltNode);
        _dataPersistence = new RoadBuilderDataPersistence(_junctionsEditor, _sectionsEditor);
    }

    public void StartBuildingRoads(Vector3 firstJunctionPosition)
    {
        _junctionsEditor.BuildJunction(firstJunctionPosition);
        _sectionsEditor.CreateNextSectionPreview();
    }

    public void UpdateNextSection(Vector3 hitPositionOnTerrain)
    {
        NextSectionStartPoint = _junctionsEditor.SelectedJunction.transform.position;
        NextSectionEndPoint = hitPositionOnTerrain;
        _sectionsEditor.UpdateNextSectionPoints(NextSectionStartPoint, NextSectionEndPoint);
        _sectionsEditor.UpdateNextSectionPreview();
    }

    public void BuildRoad()
    {
        Section builtSection = _sectionsEditor.BuildSection();
        builtSection.StartJunction = _junctionsEditor.SelectedJunction;
        _junctionsEditor.SelectedJunction.ConnectedSections.Add(builtSection);

        Junction builtJunction = _junctionsEditor.BuildJunction(NextSectionEndPoint);
        builtSection.EndJunction = builtJunction;
        builtJunction.ConnectedSections.Add(builtSection);
    }

    public void SelectJunction(Junction junction)
    {
        _junctionsEditor.SelectedJunction = junction;
    }

    public void DeleteSelectedRoad()
    {
        DeleteRoad(_junctionsEditor.SelectedJunction);
    }

    private void DeleteRoad(Junction junctionToDelete)
    {
        List<Junction> connectedJunctions = junctionToDelete.GetConnectedJunctions();
        if (connectedJunctions.Count == 0) return;

        // Once I delete a junction - I

        DeleteConnectedSections(junctionToDelete);
        Junction notDeletedJunction = DeleteEmptyJunctions(connectedJunctions);
        _junctionsEditor.DeleteJunction(junctionToDelete);
        if (notDeletedJunction != null)
        {
            _junctionsEditor.SelectedJunction = notDeletedJunction;
        }
        else
        {
            _junctionsEditor.SelectedJunction = _junctionsEditor.Junctions[0];
        }
    }

    private void DeleteConnectedSections(Junction junction)
    {
        for (int i = junction.ConnectedSections.Count - 1; i >= 0; i--)
        {
            Section section = junction.ConnectedSections[i];
            _sectionsEditor.DeleteSection(section);
        }
    }

    private Junction DeleteEmptyJunctions(List<Junction> junctions)
    {
        Junction notDeletedJunction = null;
        foreach (Junction junction in junctions)
        {
            //TODO: refactor
            bool isLastJunction = _junctionsEditor.GetNumberOfJunctions() == 2;
            if (junction.ConnectedSections.Count == 0 && !isLastJunction)
            {
                _junctionsEditor.DeleteJunction(junction);
            }
            else
            {
                notDeletedJunction = junction;
            }
        }
        return notDeletedJunction;
    }

    private Junction FindNewJunctionToSelect(List<Junction> connectedJunctions)
    {
        foreach (Junction junction in connectedJunctions)
        {
            //They are not destroyed yet
            if (junction != null)
            {
                return junction;
            }
        }
        return _junctionsEditor.Junctions[0];
    }
}
