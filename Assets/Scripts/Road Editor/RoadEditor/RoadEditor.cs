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
    private RoadBuilder _roadBuilder;

    public Vector3 SelectedJunctionPosition => _junctionsEditor.SelectedJunction.transform.position;

    public RoadEditor(RoadNodePrefabsReferencer roadNodePrefabsReferencer)
    {
        _junctionsEditor = new JunctionsEditor(roadNodePrefabsReferencer.JunctionNode);
        _sectionsEditor = new SectionsEditor(roadNodePrefabsReferencer.UnderConstructionNode, roadNodePrefabsReferencer.BuiltNode);
        _roadDeleter = new RoadDeleter(_junctionsEditor, _sectionsEditor);
        _roadBuilder = new RoadBuilder(_junctionsEditor, _sectionsEditor);
        RoadBuilderDataPersistence roadBuilderDataPersistence = new RoadBuilderDataPersistence(_junctionsEditor, _sectionsEditor);
    }

    public void StartBuildingRoads(Vector3 firstJunctionPosition)
    {
        Junction builtJunction = _junctionsEditor.BuildJunction(firstJunctionPosition);
        SelectJunction(builtJunction);
        _sectionsEditor.CreateNextSectionPreview();
    }

    public void UpdateNextSectionPreview(Vector3 hitPositionOnTerrain)
    {
        _sectionsEditor.UpdateNextSectionPreview(_junctionsEditor.SelectedJunction.transform.position, hitPositionOnTerrain);
    }

    public void BuildNewRoad(Vector3 endPoint)
    {
        Junction builtJunction = _roadBuilder.BuildRoad(_junctionsEditor.SelectedJunction, endPoint);
        SelectJunction(builtJunction);
    }

    public void BuildSectionToJunction(Junction targetJunction)
    {
        _roadBuilder.BuildSectionBetweenJunctions(_junctionsEditor.SelectedJunction, targetJunction);
    }

    public void SplitSection(Section section, Vector3 splitPosition)
    {
        Junction builtJunction = _junctionsEditor.BuildJunction(splitPosition);
        _roadBuilder.BuildSectionBetweenJunctions(_junctionsEditor.SelectedJunction, builtJunction);
        _roadBuilder.BuildSectionBetweenJunctions(builtJunction, section.StartJunction);
        _roadBuilder.BuildSectionBetweenJunctions(builtJunction, section.EndJunction);
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
