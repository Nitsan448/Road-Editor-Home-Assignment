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
    private RoadBuilderDataPersistence _dataPersistence;
    private RoadCostCalculator _roadCostCalculator;

    public Vector3 NextSectionStartPoint;
    public Vector3 NextSectionEndPoint;

    public RoadEditor(RoadNodePrefabsReferencer roadNodePrefabsReferencer, RoadCostCalculator roadCostCalculator)
    {
        _roadCostCalculator = roadCostCalculator;
        _junctionsEditor = new JunctionsEditor(roadNodePrefabsReferencer.JunctionNode);
        _sectionsEditor = new SectionsEditor(roadNodePrefabsReferencer.UnderConstructionNode, roadNodePrefabsReferencer.BuiltNode);
        _roadDeleter = new RoadDeleter(_junctionsEditor, _sectionsEditor);
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
        Debug.Log(_roadDeleter);
        _roadDeleter.DeleteSelectedRoad();
    }
}
