using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEditUIManager : MonoBehaviour
{
    private RoadEditorManager_Base _roadEditorManager;
    private RoadCostCalculator _roadCostCalculator;
    private MouseRayCastsManager _mouseRayCastsManager;

    [SerializeField] private RoadCostText _roadCostText;
    [SerializeField] private DeleteRoadButton _deleteRoadButton;
    [SerializeField] private GameObject Ui;

    public void Init(RoadEditorManager_Base roadEditorManager, RoadCostCalculator roadCostCalculator,
        MouseRayCastsManager mouseRayCastsManager)
    {
        _roadEditorManager = roadEditorManager;
        _roadCostCalculator = roadCostCalculator;
        _mouseRayCastsManager = mouseRayCastsManager;
        _roadCostText.Init(_roadCostCalculator, _mouseRayCastsManager);
        _deleteRoadButton.Init(_roadEditorManager);
    }

    public void ShowUI()
    {
        Ui.gameObject.SetActive(true);
    }

    private void Update()
    {
        _roadCostText.UpdateRoadCostText();
    }
}
