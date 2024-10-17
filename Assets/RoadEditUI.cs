using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEditUI : MonoBehaviour
{
    [SerializeField] private RoadEditorManager_Base _roadEditorManager;
    [SerializeField] private RoadCostText _roadCostText;
    [SerializeField] private RoadCostCalculator _roadCostCalculator;

    [SerializeField] private GameObject Ui;

    private void Start()
    {
        Ui.gameObject.SetActive(false);
        _roadCostText.Init(_roadCostCalculator);
    }

    private void OnEnable()
    {
        _roadEditorManager.StartedRoadEdit += ShowUI;
    }

    private void OnDisable()
    {
        _roadEditorManager.StartedRoadEdit -= ShowUI;
    }

    private void ShowUI()
    {
        Ui.gameObject.SetActive(true);
    }

    private void Update()
    {
        _roadCostText.UpdateRoadCostText();
    }
}
