using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class SectionsBuilder : IDataPersistence, IDisposable
{
    private GameObject _underConstructionNodePrefab;
    private GameObject _builtNodePrefab;
    private GameObject _sectionPreviewNode;

    private Vector3 _nextSectionStartPoint;
    private Vector3 _nextSectionEndPoint;

    private List<Section> _sections = new List<Section>();

    public SectionsBuilder(GameObject underConstructionNodePrefab, GameObject builtNodePrefab)
    {
        _underConstructionNodePrefab = underConstructionNodePrefab;
        _builtNodePrefab = builtNodePrefab;
        DataPersistenceManager.Instance.Register(this);
    }

    public void CreateNextSectionPreview()
    {
        _sectionPreviewNode = Object.Instantiate(_underConstructionNodePrefab);
    }


    public void UpdateNextSectionPoints(Vector3 startPoint, Vector3 endPoint)
    {
        _nextSectionStartPoint = startPoint;
        _nextSectionEndPoint = endPoint;
    }

    public void UpdateNextSectionPreview()
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

    public Section BuildSection()
    {
        GameObject createdObject = Object.Instantiate(_builtNodePrefab);
        SetNodeTransform(createdObject.transform);
        Section builtSection = createdObject.GetComponent<Section>();
        _sections.Add(builtSection);
        return builtSection;
    }

    public void SaveData(GameData data)
    {
        data.Sections.Clear();
        foreach (Section section in _sections)
        {
            data.Sections.Add(section.GetSectionPersistentData());
        }
    }

    public void LoadData(GameData data)
    {
        throw new System.NotImplementedException();
    }

    public void Dispose()
    {
        DataPersistenceManager.Instance.Unregister(this);
    }
}
