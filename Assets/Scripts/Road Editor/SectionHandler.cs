using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionHandler
{
    private GameObject _underConstructionNodePrefab;
    private GameObject _builtNodePrefab;
    private GameObject _sectionPreviewNode;

    public SectionHandler(GameObject underConstructionNodePrefab, GameObject builtNodePrefab)
    {
        _underConstructionNodePrefab = underConstructionNodePrefab;
        _builtNodePrefab = builtNodePrefab;
    }

    public void ShowSectionPreview(Vector3 startPoint, Vector3 endPoint)
    {
        if (_sectionPreviewNode == null)
        {
            _sectionPreviewNode = Object.Instantiate(_underConstructionNodePrefab);
        }

        SetNodeLength(_sectionPreviewNode, Vector3.Distance(startPoint, endPoint));
        _sectionPreviewNode.transform.localScale = new Vector3(_sectionPreviewNode.transform.localScale.x,
            _sectionPreviewNode.transform.localScale.y, Vector3.Distance(startPoint, endPoint));
        _sectionPreviewNode.transform.position = startPoint;
        Debug.Log(startPoint);
        Debug.Log(endPoint);
    }

    private void SetNodeLength(GameObject node, float length)
    {
        node.transform.localScale = new Vector3(node.transform.localScale.x, node.transform.localScale.y, length);
    }

    public void BuildSection(Vector3 startPoint, Vector3 endPoint)
    {

    }
}
