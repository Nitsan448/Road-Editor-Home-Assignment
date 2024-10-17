using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDataPersistence : MonoBehaviour, IDataPersistence
{
    void Start()
    {
        DataPersistenceManager.Instance.Register(this);
    }

    private void OnDestroy()
    {
        DataPersistenceManager.Instance.Unregister(this);
    }

    public void SaveData(GameData dataToSave)
    {
        dataToSave.CameraPosition = transform.position;
    }

    public void LoadData(GameData loadedData)
    {
        transform.position = loadedData.CameraPosition;
    }
}
