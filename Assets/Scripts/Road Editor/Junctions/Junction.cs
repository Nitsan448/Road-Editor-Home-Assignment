using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    public int JunctionID;
    public List<Section> ConnectedSections = new List<Section>();
}
