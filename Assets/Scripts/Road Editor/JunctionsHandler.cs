using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionsHandler
{
    private GameObject _junctionNodePrefab;
    public Junction SelectedJunction { get; private set; }

    public JunctionsHandler(GameObject junctionNodePrefab)
    {
        _junctionNodePrefab = junctionNodePrefab;
    }

    public void BuildJunction(Transform parent, Vector3 junctionPosition)
    {
        GameObject instantiatedJunction = Object.Instantiate(_junctionNodePrefab, parent);
        Debug.Log(junctionPosition);
        instantiatedJunction.transform.position = junctionPosition;
    }
}
