using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEditorMainManager : MonoBehaviour
{
    public RoadEditorManager_Base RoadEditorManager;

    void Start()
    {
        if (RoadEditorManager == null)
        {
            Debug.LogError("Could not find RoadEditorManager");
            return;
        }

        if (RoadEditorManager.Init() == false)
        {
            Debug.LogError("Could not init RoadEditorManager");
            return;
        }

        //TODO: return to original method
        // Invoke("StartRoadEditor", 3f);
        Invoke("StartRoadEditor", 1f);
    }

    void StartRoadEditor()
    {
        RoadEditorManager.StartRoadEdit();
    }
}
