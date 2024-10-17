using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEditUI : MonoBehaviour
{
    [SerializeField] private SimpleRoadEditorManager _simpleRoadEditorManager;
    [SerializeField] private RoadCostText _roadCostText;

    [SerializeField] private GameObject Ui;

    private void Start()
    {
        Ui.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _simpleRoadEditorManager.StartedRoadEdit += ShowUI;
    }

    private void OnDisable()
    {
        _simpleRoadEditorManager.StartedRoadEdit -= ShowUI;
    }

    private void ShowUI()
    {
        Ui.gameObject.SetActive(true);
    }

    private void Update()
    {
        _roadCostText.UpdateRoadCostText(_simpleRoadEditorManager);
    }
}
