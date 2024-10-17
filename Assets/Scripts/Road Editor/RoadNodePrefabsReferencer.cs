using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Road Editor/Road Node Prefabs", fileName = "Road Node Prefabs")]
public class RoadNodePrefabsReferencer : ScriptableObject
{
    public GameObject JunctionNode;
    public GameObject BuiltNode;
    public GameObject UnderConstructionNode;
}
