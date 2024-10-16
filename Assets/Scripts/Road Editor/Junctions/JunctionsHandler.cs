using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionsHandler
{
    private GameObject _junctionNodePrefab;

    public Junction SelectedJunction;

    public JunctionsHandler(GameObject junctionNodePrefab)
    {
        _junctionNodePrefab = junctionNodePrefab;
    }

    public void BuildJunction(Vector3 junctionPosition)
    {
        GameObject builtJunction = Object.Instantiate(_junctionNodePrefab);
        builtJunction.transform.position = junctionPosition;
        SelectedJunction = builtJunction.GetComponent<Junction>();
    }

    public void DeleteSelectedJunction()
    {
        Object.Destroy(SelectedJunction.gameObject);
    }
}
