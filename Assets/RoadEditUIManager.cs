using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEditUIManager : MonoBehaviour
{
    private RoadEditorManager_Base _roadEditorManager;
    [SerializeField] private RoadCostText _roadCostText;
    [SerializeField] private RoadCostCalculator _roadCostCalculator;
    [SerializeField] private DeleteRoadButton _deleteRoadButton;

    [SerializeField] private GameObject Ui;

    public void Init(RoadEditorManager_Base roadEditorManager, RoadCostCalculator roadCostCalculator)
    {
        _roadEditorManager = roadEditorManager;
        _roadCostCalculator = roadCostCalculator;
        _roadCostText.Init(_roadCostCalculator);
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
