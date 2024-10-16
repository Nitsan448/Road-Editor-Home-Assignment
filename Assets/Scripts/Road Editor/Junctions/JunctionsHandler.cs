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

    public void BuildFirstJunction(Vector3 junctionPosition)
    {
        GameObject builtJunction = Object.Instantiate(_junctionNodePrefab);
        builtJunction.transform.position = junctionPosition;
        SelectedJunction = builtJunction.GetComponent<Junction>();
    }

    public void BuildJunction(Vector3 junctionPosition, Section connectedSection)
    {
        Junction previousJunction = SelectedJunction;
        previousJunction.ConnectedSections.Add(connectedSection);
        GameObject builtJunction = Object.Instantiate(_junctionNodePrefab);
        builtJunction.transform.position = junctionPosition;
        SelectedJunction = builtJunction.GetComponent<Junction>();
        SelectedJunction.ConnectedSections.Add(connectedSection);
    }

    public void DeleteSelectedJunction()
    {
        Object.Destroy(SelectedJunction.gameObject);
    }
}
