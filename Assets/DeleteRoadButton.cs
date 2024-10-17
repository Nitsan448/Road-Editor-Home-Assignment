using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteRoadButton : MonoBehaviour
{
    private RoadEditorManager_Base _roadEditorManager;

    public void Init(RoadEditorManager_Base roadEditorManager)
    {
        _roadEditorManager = roadEditorManager;
    }
}
