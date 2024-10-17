using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//TODO: refactor
public class FileDataHandler
{
    private string _saveFileDirectoryPath;
    private string _saveFileName;

    public FileDataHandler(string saveFileDirectoryPath, string saveFileName)
    {
        _saveFileDirectoryPath = saveFileDirectoryPath;
        _saveFileName = saveFileName;
    }

    public GameData TryLoadingData()
    {
        string fullPath = GetFullDataFilePath();
        if (!File.Exists(fullPath))
        {
            return null;
        }

        try
        {
            return LoadData(fullPath);
        }
        catch (Exception e)
        {
            Debug.LogError("Could not load data from file " + fullPath + "\n" + e.Message);
            return null;
        }
    }

    private GameData LoadData(string fullPath)
    {
        string dataToLoad = "";
        using (StreamReader reader = new StreamReader(fullPath))
        {
            dataToLoad = reader.ReadToEnd();
        }


        return JsonUtility.FromJson<GameData>(dataToLoad);
    }


    public void Save(GameData data)
    {
        string fullPath = GetFullDataFilePath();
        try
        {
            //TODO: understand why not use directory path
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.Write(dataToStore);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Could not save data to file " + fullPath + "\n" + e.Message);
        }
    }

    private string GetFullDataFilePath()
    {
        return Path.Combine(_saveFileDirectoryPath, _saveFileName);
    }
}
