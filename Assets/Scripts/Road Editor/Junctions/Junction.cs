using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    public int Id;
    public List<Section> ConnectedSections = new List<Section>();

    public JunctionPersistentData GetJunctionPersistentData()
    {
        return new JunctionPersistentData(Id, transform.position);
    }
}
