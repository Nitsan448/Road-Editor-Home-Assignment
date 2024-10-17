using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    [HideInInspector] public int Id;
    public List<Section> ConnectedSections = new List<Section>();

    public JunctionPersistentData GetJunctionPersistentData()
    {
        return new JunctionPersistentData(Id, transform.position);
    }

    public List<Junction> GetConnectedJunctions()
    {
        List<Junction> connectedJunctions = new List<Junction>();
        foreach (Section connectedSection in ConnectedSections)
        {
            if (connectedSection.StartJunction == this)
            {
                connectedJunctions.Add(connectedSection.EndJunction);
            }
            if (connectedSection.EndJunction == this)
            {
                connectedJunctions.Add(connectedSection.StartJunction);
            }
        }

        return connectedJunctions;
    }
}
