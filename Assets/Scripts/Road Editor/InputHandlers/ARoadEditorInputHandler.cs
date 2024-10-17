using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ARoadEditorInputHandler : MonoBehaviour
{
    protected MouseRayCastsManager _mouseRayCastsManager;
    protected RoadEditor _roadEditor;
    protected RoadCostCalculator _roadCostCalculator;

    public void Init(MouseRayCastsManager mouseRayCastsManager, RoadEditor roadEditor, RoadCostCalculator roadCostCalculator)
    {
        _mouseRayCastsManager = mouseRayCastsManager;
        _roadEditor = roadEditor;
        _roadCostCalculator = roadCostCalculator;
    }


    public abstract void ReactToInput();
}
