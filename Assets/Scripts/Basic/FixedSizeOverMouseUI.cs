using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FixedSizeOverMouseUI : MonoBehaviour
{
    [SerializeField] private Vector3 _screenPositionOffset;
    [SerializeField] private MouseRayCastsManager _mouseRayCastsManager;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(_mouseRayCastsManager.HitPositionOnTerrain);
        screenPosition += _screenPositionOffset;

        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
        transform.position = worldPosition;
    }

}
