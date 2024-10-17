using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SectionPersistentData
{
    public int StartJunctionId;
    public int EndJunctionId;

    public SectionPersistentData(int startJunctionId, int endJunctionId)
    {
        StartJunctionId = startJunctionId;
        EndJunctionId = endJunctionId;
    }
}
