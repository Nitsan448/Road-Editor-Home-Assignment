using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: refactor
public class RoadDeleter
{
    private JunctionsEditor _junctionsEditor;
    private SectionsEditor _sectionsEditor;

    public RoadDeleter(JunctionsEditor junctionsEditor, SectionsEditor sectionsEditor)
    {
        _junctionsEditor = junctionsEditor;
        _sectionsEditor = sectionsEditor;
    }

    public void DeleteRoad(Junction junctionToDelete)
    {
        List<Junction> connectedJunctions = junctionToDelete.GetConnectedJunctions();
        if (connectedJunctions.Count == 0) return;

        DeleteConnectedSections(junctionToDelete);
        _junctionsEditor.DeleteJunction(junctionToDelete);
        Junction notDeletedJunction = DeleteEmptyJunctions(connectedJunctions);
        _junctionsEditor.SelectedJunction = notDeletedJunction != null ? notDeletedJunction : _junctionsEditor.Junctions[0];
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
            bool isLastJunction = _junctionsEditor.GetNumberOfJunctions() == 1;
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
}
