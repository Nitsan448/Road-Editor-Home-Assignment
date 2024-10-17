using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    public void SaveData(GameData dataToSave);

    public void LoadData(GameData loadedData);
}
