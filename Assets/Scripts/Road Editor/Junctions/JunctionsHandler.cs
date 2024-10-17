using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class JunctionsHandler : IDataPersistence, IDisposable
{
    public Junction SelectedJunction;

    private GameObject _junctionNodePrefab;
    private int _lastBuiltJunctionId = 0;

    private List<Junction> _junctions = new List<Junction>();

    public JunctionsHandler(GameObject junctionNodePrefab)
    {
        _junctionNodePrefab = junctionNodePrefab;
        DataPersistenceManager.Instance.Register(this);
    }

    public void BuildJunction(Vector3 junctionPosition)
    {
        GameObject builtJunction = Object.Instantiate(_junctionNodePrefab);
        builtJunction.transform.position = junctionPosition;
        SelectedJunction = builtJunction.GetComponent<Junction>();
        SelectedJunction.Id = _lastBuiltJunctionId;
        _lastBuiltJunctionId++;
        _junctions.Add(SelectedJunction);
    }

    public void DeleteSelectedJunction()
    {
        Object.Destroy(SelectedJunction.gameObject);
    }


    public void SaveData(GameData data)
    {
        data.Junctions.Clear();
        foreach (Junction junction in _junctions)
        {
            data.Junctions.Add(junction.GetJunctionPersistentData());
            if (SelectedJunction == junction)
            {
                data.SelectedJunctionId = junction.Id;
            }
        }
    }

    public void LoadData(GameData data)
    {
        foreach (JunctionPersistentData junctionData in data.Junctions)
        {
            BuildJunction(junctionData.Position);
            SelectedJunction.Id = junctionData.Id;
        }
    }

    public void Dispose()
    {
        DataPersistenceManager.Instance.Unregister(this);
    }
}
