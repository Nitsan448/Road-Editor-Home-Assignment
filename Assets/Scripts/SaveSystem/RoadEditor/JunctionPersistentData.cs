using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JunctionPersistentData
{
    public int Id;
    public Vector3 Position;

    public JunctionPersistentData(int id, Vector3 position)
    {
        Id = id;
        Position = position;
    }
}
