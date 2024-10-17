using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionsHandler
{
    public Junction SelectedJunction;

    private GameObject _junctionNodePrefab;
    private int _lastBuiltJunctionId = 0;

    public JunctionsHandler(GameObject junctionNodePrefab)
    {
        _junctionNodePrefab = junctionNodePrefab;
    }

    public void BuildJunction(Vector3 junctionPosition)
    {
        GameObject builtJunction = Object.Instantiate(_junctionNodePrefab);
        builtJunction.transform.position = junctionPosition;
        SelectedJunction = builtJunction.GetComponent<Junction>();
        SelectedJunction.JunctionID = _lastBuiltJunctionId;
        _lastBuiltJunctionId++;
    }

    public void DeleteSelectedJunction()
    {
        Object.Destroy(SelectedJunction.gameObject);
    }
}
