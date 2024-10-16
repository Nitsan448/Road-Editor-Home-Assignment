using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionsBuilder
{
    private GameObject _underConstructionNodePrefab;
    private GameObject _builtNodePrefab;
    private GameObject _sectionPreviewNode;

    private Vector3 _nextSectionStartPoint;
    private Vector3 _nextSectionEndPoint;

    public SectionsBuilder(GameObject underConstructionNodePrefab, GameObject builtNodePrefab)
    {
        _underConstructionNodePrefab = underConstructionNodePrefab;
        _builtNodePrefab = builtNodePrefab;
    }

    public void CreateNextSectionPreview()
    {
        _sectionPreviewNode = Object.Instantiate(_underConstructionNodePrefab);
    }

    public void Update(Vector3 startPoint, Vector3 endPoint)
    {
        _nextSectionStartPoint = startPoint;
        _nextSectionEndPoint = endPoint;
        UpdateNextSectionPreview();
    }

    private void UpdateNextSectionPreview()
    {
        SetNodeTransform(_sectionPreviewNode.transform);
    }


    private void SetNodeTransform(Transform node)
    {
        node.transform.position = _nextSectionStartPoint;
        SetNodeLength(node);
        SetNodeRotation(node);
    }

    private void SetNodeLength(Transform node)
    {
        float length = Vector3.Distance(_nextSectionStartPoint, _nextSectionEndPoint);
        node.localScale = new Vector3(node.localScale.x, node.localScale.y, length);
    }

    private void SetNodeRotation(Transform node)
    {
        Vector3 direction = _nextSectionEndPoint - _nextSectionStartPoint;
        if (direction.magnitude <= Mathf.Epsilon) return;
        node.rotation = Quaternion.LookRotation(direction);
    }

    public void BuildSection()
    {
        GameObject builtSection = Object.Instantiate(_builtNodePrefab);
        SetNodeTransform(builtSection.transform);
    }
}
