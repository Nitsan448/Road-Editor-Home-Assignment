using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<SectionPersistentData> Sections;
    public List<JunctionPersistentData> Junctions;
    public Vector3 CameraPosition;
    public int SelectedJunctionId;

    public GameData()
    {
        Sections = new List<SectionPersistentData>();
        Junctions = new List<JunctionPersistentData>();
        SelectedJunctionId = 0;
    }
}
