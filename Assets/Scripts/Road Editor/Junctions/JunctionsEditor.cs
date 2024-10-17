using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class JunctionsEditor
{
    public Junction SelectedJunction;

    private GameObject _junctionNodePrefab;
    private int _lastBuiltJunctionId = 0;

    public List<Junction> Junctions { get; private set; } = new List<Junction>();

    public JunctionsEditor(GameObject junctionNodePrefab)
    {
        _junctionNodePrefab = junctionNodePrefab;
    }

    public Junction BuildJunction(Vector3 junctionPosition)
    {
        GameObject createdObject = Object.Instantiate(_junctionNodePrefab);
        createdObject.transform.position = junctionPosition;
        Junction builtJunction = createdObject.GetComponent<Junction>();

        builtJunction.Id = _lastBuiltJunctionId;
        _lastBuiltJunctionId++;
        Junctions.Add(builtJunction);

        return builtJunction;
    }

    public void DeleteJunction(Junction junction)
    {
        Junctions.Remove(junction);
        Object.Destroy(junction.gameObject);
    }

    public int GetNumberOfJunctions()
    {
        return Junctions.Count;
    }

}
