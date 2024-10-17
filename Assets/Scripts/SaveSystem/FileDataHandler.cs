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

    public GameData Load()
    {
        string fullPath = Path.Combine(_saveFileDirectoryPath, _saveFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Could not load data from file " + fullPath + "\n" + e.Message);
            }
        }

        return loadedData;
    }


    public void Save(GameData data)
    {
        string fullPath = Path.Combine(_saveFileDirectoryPath, _saveFileName);
        try
        {
            //TODO: understand why not use directory path
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            //TODO: use close?
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Could not save data to file " + fullPath + "\n" + e.Message);
        }
    }
}
