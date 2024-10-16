using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISectionConnector
{
    public List<Section> ConnectedSections { get; set; }
}
