using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private Vector3 _offsetFromMouse;

    public void UpdatePosition(Vector3 mousePosition)
    {
        //TODO: Cache camera.main outside of method.
        Camera camera = Camera.main;
        Vector3 screenPosition = camera.WorldToScreenPoint(mousePosition);
        screenPosition.y += _offsetFromMouse.y;

        Vector3 worldPosition = camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
        transform.position = worldPosition;
    }

    public void UpdateText(string newText)
    {
        _text.text = newText;
    }
}
