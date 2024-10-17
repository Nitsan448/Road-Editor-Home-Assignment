using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO: make this not a singleton
public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance { get; private set; }

    [SerializeField] private string _saveFileName;
    private FileDataHandler _fileDataHandler;
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
        _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _saveFileName);
        NewGame();
        Invoke("SaveGame", 3f);
        // LoadGame();
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
        Debug.Log("Saving");
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(_gameData);
        }

        //TODO: extract from here
        _gameData.CameraPosition = Camera.main.transform.position;
        _fileDataHandler.Save(_gameData);
    }

    public void LoadGame()
    {
        _gameData = _fileDataHandler.Load();
        if (_gameData == null)
        {
            return;
        }
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(_gameData);
        }

        //TODO: extract from here
        Camera.main.transform.position = _gameData.CameraPosition;
    }
}
