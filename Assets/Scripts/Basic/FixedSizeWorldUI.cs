using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FixedSizeWorldUI : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    public void UpdatePosition(Vector3 newPosition)
    {
        //TODO: Cache camera.main outside of method.
        Camera camera = Camera.main;
        Vector3 screenPosition = camera.WorldToScreenPoint(newPosition);
        screenPosition += _offset;

        Vector3 worldPosition = camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
        transform.position = worldPosition;
    }
}
