using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<SectionPersistentData> Sections;

    public GameData()
    {
        Sections = new List<SectionPersistentData>();
    }
}
