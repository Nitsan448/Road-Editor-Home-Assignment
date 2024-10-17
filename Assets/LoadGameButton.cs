using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(DataPersistenceManager.Instance.LoadGame);
    }

}
