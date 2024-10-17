using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class RoadEditor
{
    private JunctionsEditor _junctionsEditor;
    private SectionsEditor _sectionsEditor;
    private RoadDeleter _roadDeleter;

    public Vector3 NextSectionStartPoint;
    public Vector3 NextSectionEndPoint;

    public RoadEditor(RoadNodePrefabsReferencer roadNodePrefabsReferencer)
    {
        _junctionsEditor = new JunctionsEditor(roadNodePrefabsReferencer.JunctionNode);
        _sectionsEditor = new SectionsEditor(roadNodePrefabsReferencer.UnderConstructionNode, roadNodePrefabsReferencer.BuiltNode);
        _roadDeleter = new RoadDeleter(_junctionsEditor, _sectionsEditor);
        RoadBuilderDataPersistence roadBuilderDataPersistence = new RoadBuilderDataPersistence(_junctionsEditor, _sectionsEditor);
    }

    public void StartBuildingRoads(Vector3 firstJunctionPosition)
    {
        Junction builtJunction = _junctionsEditor.BuildJunction(firstJunctionPosition);
        SelectJunction(builtJunction);
        _sectionsEditor.CreateNextSectionPreview();
    }

    public void UpdateNextSection(Vector3 hitPositionOnTerrain)
    {
        NextSectionStartPoint = _junctionsEditor.SelectedJunction.transform.position;
        NextSectionEndPoint = hitPositionOnTerrain;
        _sectionsEditor.UpdateNextSectionPreview(NextSectionStartPoint, NextSectionEndPoint);
    }

    public void BuildSectionToJunction(Junction targetJunction)
    {
        BuildSectionBetweenJunctions(_junctionsEditor.SelectedJunction, targetJunction);
    }

    public void BuildSectionBetweenJunctions(Junction startJunction, Junction endJunction)
    {
        Section builtSection = _sectionsEditor.BuildSection(startJunction.transform.position, endJunction.transform.position);
        builtSection.StartJunction = startJunction;
        startJunction.ConnectedSections.Add(builtSection);

        builtSection.EndJunction = endJunction;
        endJunction.ConnectedSections.Add(builtSection);
    }

    public void BuildNewRoad()
    {
        Junction builtJunction = _junctionsEditor.BuildJunction(NextSectionEndPoint);
        BuildSectionToJunction(builtJunction);
        SelectJunction(builtJunction);
    }


    public void SplitSection(Section section, Vector3 splitPosition)
    {
        Junction builtJunction = _junctionsEditor.BuildJunction(splitPosition);
        BuildSectionBetweenJunctions(_junctionsEditor.SelectedJunction, builtJunction);
        BuildSectionBetweenJunctions(builtJunction, section.StartJunction);
        BuildSectionBetweenJunctions(builtJunction, section.EndJunction);
        SelectJunction(builtJunction);
        section.Delete();
    }

    public void SelectJunction(Junction junction)
    {
        _junctionsEditor.SelectedJunction = junction;
    }

    public void DeleteSelectedRoad()
    {
        _roadDeleter.DeleteSelectedRoad();
    }
}
