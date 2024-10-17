using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveTest : MonoBehaviour, IDataPersistence
{
    private int _deathCount = 10;

    private void Start()
    {
        DataPersistenceManager.Instance.Register(this);
    }

    private void OnDestroy()
    {
        DataPersistenceManager.Instance.Unregister(this);
    }

    public void SaveData(GameData data)
    {
        data.DeathCount = _deathCount;
    }

    public void LoadData(GameData data)
    {
        _deathCount = data.DeathCount;
    }

}
