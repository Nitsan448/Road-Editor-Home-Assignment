using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(DataPersistenceManager.Instance.SaveGame);
    }
}
