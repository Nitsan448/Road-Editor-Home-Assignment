using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO: make this not a singleton
public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance { get; private set; }
    private List<IDataPersistence> _dataPersistenceObjects = new List<IDataPersistence>();
    private GameData _gameData;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Existing Data Persistence Manager found, destroying the new one");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        NewGame();
        SaveGame();
        LoadGame();
    }

    public void Register(IDataPersistence dataPersistenceObject)
    {
        _dataPersistenceObjects.Add(dataPersistenceObject);
    }

    public void Unregister(IDataPersistence dataPersistenceObject)
    {
        _dataPersistenceObjects.Remove(dataPersistenceObject);
    }


    public void NewGame()
    {
        _gameData = new GameData();
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(_gameData);
        }
    }

    public void LoadGame()
    {
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(_gameData);
        }
    }
}
