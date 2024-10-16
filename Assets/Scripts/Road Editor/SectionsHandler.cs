using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionsHandler
{
    private GameObject _underConstructionNodePrefab;
    private GameObject _builtNodePrefab;
    private GameObject _sectionPreviewNode;

    public SectionsHandler(GameObject underConstructionNodePrefab, GameObject builtNodePrefab)
    {
        _underConstructionNodePrefab = underConstructionNodePrefab;
        _builtNodePrefab = builtNodePrefab;
    }

    public void CreateSectionPreview()
    {
        _sectionPreviewNode = Object.Instantiate(_underConstructionNodePrefab);
    }

    public void UpdateSectionPreview(Vector3 startPoint, Vector3 endPoint)
    {
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
        // _sectionPreviewNode.SetActive(false);
        GameObject builtSection = Object.Instantiate(_builtNodePrefab);
        SetNodeTransform(builtSection.transform, startPoint, endPoint);
    }
}
