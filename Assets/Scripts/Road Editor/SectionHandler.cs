using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionHandler
{
    private GameObject _underConstructionNodePrefab;
    private GameObject _builtNodePrefab;

    public SectionHandler(GameObject underConstructionNodePrefab, GameObject builtNodePrefab)
    {
        _underConstructionNodePrefab = underConstructionNodePrefab;
        _builtNodePrefab = builtNodePrefab;
    }

    public void ShowSectionPreview(Vector3 startPoint, Vector3 endPoint)
    {
        Debug.Log(startPoint);
        Debug.Log(endPoint);
    }

    public void BuildSection(Vector3 startPoint, Vector3 endPoint)
    {

    }
}
