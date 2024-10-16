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

        SetNodeTransform(_sectionPreviewNode.transform, startPoint, endPoint);
    }

    private void SetNodeTransform(Transform node, Vector3 startPoint, Vector3 endPoint)
    {
        node.transform.position = startPoint;
        SetNodeLength(node, startPoint, endPoint);
        SetNodeRotation(node, startPoint, endPoint);
    }

    private void SetNodeLength(Transform node, Vector3 startPoint, Vector3 endPoint)
    {
        float length = Vector3.Distance(startPoint, endPoint);
        node.localScale = new Vector3(node.localScale.x, node.localScale.y, length);
    }

    //TODO: Extract code to different script?
    private void SetNodeRotation(Transform node, Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 direction = endPoint - startPoint;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        node.rotation = targetRotation;
    }

    public void BuildSection(Vector3 startPoint, Vector3 endPoint)
    {
        GameObject builtSection = Object.Instantiate(_builtNodePrefab);
        SetNodeTransform(builtSection.transform, startPoint, endPoint);
    }
}
