using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilder
{
    private JunctionsEditor _junctionsEditor;
    private SectionsEditor _sectionsEditor;

    public RoadBuilder(JunctionsEditor junctionsEditor, SectionsEditor sectionsEditor)
    {
        _junctionsEditor = junctionsEditor;
        _sectionsEditor = sectionsEditor;
    }

    public Junction BuildRoad(Junction startJunction, Vector3 endPoint)
    {
        Junction builtJunction = _junctionsEditor.BuildJunction(endPoint);
        BuildSectionBetweenJunctions(startJunction, builtJunction);
        return builtJunction;
    }

    public void BuildSectionBetweenJunctions(Junction startJunction, Junction endJunction)
    {
        Section builtSection = _sectionsEditor.BuildSection(startJunction.transform.position, endJunction.transform.position);
        builtSection.StartJunction = startJunction;
        startJunction.ConnectedSections.Add(builtSection);

        builtSection.EndJunction = endJunction;
        endJunction.ConnectedSections.Add(builtSection);
    }

}
