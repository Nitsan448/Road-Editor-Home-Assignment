using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FixedSizeOverMouseUI : MonoBehaviour
{
    [SerializeField] private Vector3 _screenPositionOffset;
    [SerializeField] private MouseRayCaster _mouseRayCaster;

    private void Update()
    {
        //TODO: Cache camera.main outside of method.
        Camera camera = Camera.main;
        Vector3 screenPosition = camera.WorldToScreenPoint(_mouseRayCaster.HitPositionOnTerrain);
        screenPosition += _screenPositionOffset;

        Vector3 worldPosition = camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
        transform.position = worldPosition;
    }

}
