using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionsHandler
{
    private GameObject _junctionNodePrefab;

    // private Queue<Junction> _builtJunctions = new Queue<Junction>();
    public Junction SelectedJunction;

    public JunctionsHandler(GameObject junctionNodePrefab)
    {
        _junctionNodePrefab = junctionNodePrefab;
    }

    public void BuildJunction(Transform parent, Vector3 junctionPosition)
    {
        GameObject builtJunction = Object.Instantiate(_junctionNodePrefab, parent);
        builtJunction.transform.position = junctionPosition;
        SelectedJunction = builtJunction.GetComponent<Junction>();
        // _builtJunctions.Enqueue(builtJunction.GetComponent<Junction>());
    }

    public void DeleteLastJunction()
    {
        // Junction junctionToDelete = _builtJunctions.Dequeue();
        // Object.Destroy(junctionToDelete.gameObject);
        Object.Destroy(SelectedJunction.gameObject);
    }

    // public Junction GetSelectedJunction()
    // {
    //     Debug.Log(_builtJunctions.Count > 0 ? _builtJunctions.Peek().transform.position : Vector3.zero);
    //     return _builtJunctions.Count > 0 ? _builtJunctions.Peek() : null;
    // }
}
