using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilderDataPersistence : IDataPersistence, IDisposable
{
    private JunctionsEditor _junctionsEditor;
    private SectionsEditor _sectionsEditor;
    private Dictionary<int, Junction> _junctionsByIds = new Dictionary<int, Junction>();

    public RoadBuilderDataPersistence(JunctionsEditor junctionsEditor, SectionsEditor sectionsEditor)
    {
        _junctionsEditor = junctionsEditor;
        _sectionsEditor = sectionsEditor;
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
        foreach (Junction junction in _junctionsEditor.Junctions)
        {
            data.Junctions.Add(junction.GetJunctionPersistentData());

            //TODO: Fix this
            //TODO: check if this was fixed
            if (_junctionsEditor.SelectedJunction == junction)
            {
                data.SelectedJunctionId = junction.Id;
            }
        }
    }

    private void SaveSectionsData(GameData data)
    {
        data.Sections.Clear();
        foreach (Section section in _sectionsEditor.Sections)
        {
            data.Sections.Add(section.GetSectionPersistentData());
        }
    }

    public void LoadData(GameData loadedData)
    {
        LoadJunctionsData(loadedData);
        LoadSectionsData(loadedData);
        _junctionsEditor.SelectedJunction = _junctionsByIds[loadedData.SelectedJunctionId];
    }

    private void LoadJunctionsData(GameData loadedData)
    {
        for (int i = _junctionsEditor.Junctions.Count - 1; i >= 0; i--)
        {
            Junction junction = _junctionsEditor.Junctions[i];
            _junctionsEditor.DeleteJunction(junction);
        }
        foreach (JunctionPersistentData junctionData in loadedData.Junctions)
        {
            Junction builtJunction = _junctionsEditor.BuildJunction(junctionData.Position);
            builtJunction.Id = junctionData.Id;
            _junctionsByIds[junctionData.Id] = builtJunction;
        }
    }

    private void LoadSectionsData(GameData loadedData)
    {
        for (int i = _sectionsEditor.Sections.Count - 1; i >= 0; i--)
        {
            Section section = _sectionsEditor.Sections[i];
            _sectionsEditor.DeleteSection(section);
        }
        foreach (SectionPersistentData sectionData in loadedData.Sections)
        {
            Junction startJunction = _junctionsByIds[sectionData.StartJunctionId];
            Junction endJunction = _junctionsByIds[sectionData.EndJunctionId];
            _sectionsEditor.UpdateNextSectionPoints(startJunction.transform.position, endJunction.transform.position);
            Section builtSection = _sectionsEditor.BuildSection();
            builtSection.StartJunction = startJunction;
            builtSection.EndJunction = endJunction;
            startJunction.ConnectedSections.Add(builtSection);
            endJunction.ConnectedSections.Add(builtSection);
        }
    }
}
