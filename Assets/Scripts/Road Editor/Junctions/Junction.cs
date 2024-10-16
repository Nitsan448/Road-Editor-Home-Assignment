using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour, ISectionConnector
{
    public List<Section> ConnectedSections { get; set; } = new List<Section>();
}
