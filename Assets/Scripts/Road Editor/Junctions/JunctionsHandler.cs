using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class JunctionsHandler
{
    public Junction SelectedJunction;

    private GameObject _junctionNodePrefab;
    private int _lastBuiltJunctionId = 0;

    public List<Junction> Junctions { get; private set; } = new List<Junction>();

    public JunctionsHandler(GameObject junctionNodePrefab)
    {
        _junctionNodePrefab = junctionNodePrefab;
    }

    public void BuildJunction(Vector3 junctionPosition)
    {
        GameObject builtJunction = Object.Instantiate(_junctionNodePrefab);
        builtJunction.transform.position = junctionPosition;
        SelectedJunction = builtJunction.GetComponent<Junction>();
        SelectedJunction.Id = _lastBuiltJunctionId;
        _lastBuiltJunctionId++;
        Junctions.Add(SelectedJunction);
    }

    public void DeleteJunction(Junction junction)
    {
        Object.Destroy(junction.gameObject);
    }

}
