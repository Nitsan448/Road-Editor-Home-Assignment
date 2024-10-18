using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class SectionsEditor
{
    public List<Section> Sections { get; private set; } = new List<Section>();

    private GameObject _underConstructionNodePrefab;
    private GameObject _builtNodePrefab;
    private GameObject _sectionPreviewNode;
    private Transform _builtRoadsParent;


    public SectionsEditor(GameObject underConstructionNodePrefab, GameObject builtNodePrefab, Transform builtRoadsParent)
    {
        _underConstructionNodePrefab = underConstructionNodePrefab;
        _builtNodePrefab = builtNodePrefab;
        _builtRoadsParent = builtRoadsParent;
    }

    public void CreateNextSectionPreview()
    {
        _sectionPreviewNode = Object.Instantiate(_underConstructionNodePrefab, _builtRoadsParent, true);
    }

    public void UpdateNextSectionPreview(Vector3 startPoint, Vector3 endPoint)
    {
        SetSectionTransformValues(_sectionPreviewNode.transform, startPoint, endPoint);
    }

    private void SetSectionTransformValues(Transform sectionTransform, Vector3 startPoint, Vector3 endPoint)
    {
        sectionTransform.position = startPoint;
        SetNodeLength(sectionTransform, startPoint, endPoint);
        SetNodeRotation(sectionTransform, startPoint, endPoint);
    }

    private void SetNodeLength(Transform sectionTransform, Vector3 startPoint, Vector3 endPoint)
    {
        float length = Vector3.Distance(startPoint, endPoint);
        sectionTransform.localScale = new Vector3(sectionTransform.localScale.x, sectionTransform.localScale.y, length);
    }

    private void SetNodeRotation(Transform sectionTransform, Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 direction = endPoint - startPoint;
        if (direction.magnitude <= Mathf.Epsilon) return;
        sectionTransform.rotation = Quaternion.LookRotation(direction);
    }

    public Section BuildSection(Vector3 startPoint, Vector3 endPoint)
    {
        GameObject createdObject = Object.Instantiate(_builtNodePrefab);
        SetSectionTransformValues(createdObject.transform, startPoint, endPoint);
        Section builtSection = createdObject.GetComponent<Section>();
        Sections.Add(builtSection);
        builtSection.name = "Section " + (Sections.Count + 1);
        builtSection.transform.parent = _builtRoadsParent;
        return builtSection;
    }

    public void DeleteSection(Section section)
    {
        Sections.Remove(section);
        section.DisconnectFromJunctions();
        Object.Destroy(section.gameObject);
    }
}
