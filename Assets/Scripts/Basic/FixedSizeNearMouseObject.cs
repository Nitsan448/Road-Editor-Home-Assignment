using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FixedSizeNearMouseObject : MonoBehaviour
{
    [SerializeField] private Vector2 _screenPositionOffset;

    private MouseRayCastsManager _mouseRayCastsManager;
    private Camera _mainCamera;

    public void Init(MouseRayCastsManager mouseRayCastsManager)
    {
        _mainCamera = Camera.main;
        _mouseRayCastsManager = mouseRayCastsManager;
    }

    private void Update()
    {
        Vector2 screenPosition = _mainCamera.WorldToScreenPoint(_mouseRayCastsManager.HitPositionOnTerrain);
        screenPosition += _screenPositionOffset;

        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
        transform.position = worldPosition;
    }

}
