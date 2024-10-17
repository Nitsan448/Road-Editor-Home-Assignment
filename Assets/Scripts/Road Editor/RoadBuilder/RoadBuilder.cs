using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilder : IDataPersistence, IDisposable
{
    private JunctionsHandler _junctionsHandler;
    private SectionsBuilder _sectionsBuilder;
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
        DataPersistenceManager.Instance.Register(this);
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
        _junctionsHandler.BuildJunction(NextSectionEndPoint);
        builtSection.EndJunction = _junctionsHandler.SelectedJunction;
    }

    public void SelectJunction(Junction junction)
    {
        _junctionsHandler.SelectedJunction = junction;
    }

    public void Dispose()
    {
        DataPersistenceManager.Instance.Unregister(this);
    }

    public void SaveData(GameData dataToSave)
    {
        SaveJunctionsData(dataToSave);
        SaveSectionsData(dataToSave);
    }

    private void SaveJunctionsData(GameData data)
    {
        data.Junctions.Clear();
        foreach (Junction junction in _junctionsHandler.Junctions)
        {
            data.Junctions.Add(junction.GetJunctionPersistentData());
            if (_junctionsHandler.SelectedJunction == junction)
            {
                data.SelectedJunctionId = junction.Id;
            }
        }
    }

    private void SaveSectionsData(GameData data)
    {
        data.Sections.Clear();
        foreach (Section section in _sectionsBuilder.Sections)
        {
            data.Sections.Add(section.GetSectionPersistentData());
        }
    }

    public void LoadData(GameData loadedData)
    {
        LoadJunctionsData(loadedData);
        LoadSectionsData(loadedData);
    }

    private void LoadJunctionsData(GameData loadedData)
    {
        foreach (Junction junction in _junctionsHandler.Junctions)
        {
            _junctionsHandler.DeleteJunction(junction);
        }
        foreach (JunctionPersistentData junctionData in loadedData.Junctions)
        {
            _junctionsHandler.BuildJunction(junctionData.Position);
            _junctionsHandler.SelectedJunction.Id = junctionData.Id;
        }
    }

    private void LoadSectionsData(GameData loadedData)
    {
        foreach (Section section in _sectionsBuilder.Sections)
        {
            _sectionsBuilder.DeleteSection(section);
        }
        foreach (SectionPersistentData sectionData in loadedData.Sections)
        {
            // UpdateNextSectionPoints(sectionData.StartJunctionId);
        }
    }
}
