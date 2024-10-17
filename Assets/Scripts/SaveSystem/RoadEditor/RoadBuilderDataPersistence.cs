using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilderDataPersistence : IDataPersistence, IDisposable
{
    private JunctionsHandler _junctionsHandler;
    private SectionsHandler _sectionsHandler;
    private Dictionary<int, Junction> _junctionsByIds = new Dictionary<int, Junction>();

    public RoadBuilderDataPersistence(JunctionsHandler junctionsHandler, SectionsHandler sectionsHandler)
    {
        _junctionsHandler = junctionsHandler;
        _sectionsHandler = sectionsHandler;
        DataPersistenceManager.Instance.Register(this);
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

            //TODO: Fix this
            //TODO: check if this was fixed
            if (_junctionsHandler.SelectedJunction == junction)
            {
                data.SelectedJunctionId = junction.Id;
            }
        }
    }

    private void SaveSectionsData(GameData data)
    {
        data.Sections.Clear();
        foreach (Section section in _sectionsHandler.Sections)
        {
            data.Sections.Add(section.GetSectionPersistentData());
        }
    }

    public void LoadData(GameData loadedData)
    {
        LoadJunctionsData(loadedData);
        LoadSectionsData(loadedData);
        _junctionsHandler.SelectedJunction = _junctionsByIds[loadedData.SelectedJunctionId];
    }

    private void LoadJunctionsData(GameData loadedData)
    {
        foreach (Junction junction in _junctionsHandler.Junctions)
        {
            _junctionsHandler.DeleteJunction(junction);
        }
        foreach (JunctionPersistentData junctionData in loadedData.Junctions)
        {
            Junction builtJunction = _junctionsHandler.BuildJunction(junctionData.Position);
            builtJunction.Id = junctionData.Id;
            _junctionsByIds[junctionData.Id] = builtJunction;
        }
    }

    private void LoadSectionsData(GameData loadedData)
    {
        foreach (Section section in _sectionsHandler.Sections)
        {
            _sectionsHandler.DeleteSection(section);
        }
        foreach (SectionPersistentData sectionData in loadedData.Sections)
        {
            Junction startJunction = _junctionsByIds[sectionData.StartJunctionId];
            Junction endJunction = _junctionsByIds[sectionData.EndJunctionId];
            _sectionsHandler.UpdateNextSectionPoints(startJunction.transform.position, endJunction.transform.position);
            Section builtSection = _sectionsHandler.BuildSection();
            builtSection.StartJunction = startJunction;
            builtSection.EndJunction = endJunction;
            startJunction.ConnectedSections.Add(builtSection);
            endJunction.ConnectedSections.Add(builtSection);
        }
    }
}
