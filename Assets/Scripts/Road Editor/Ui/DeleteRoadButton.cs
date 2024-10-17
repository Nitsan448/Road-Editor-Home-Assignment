using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteRoadButton : MonoBehaviour
{
    public void Init(RoadEditorManager roadEditorManager)
    {
        GetComponent<Button>().onClick.AddListener(roadEditorManager.DeleteSelectedRoad);
    }
}
