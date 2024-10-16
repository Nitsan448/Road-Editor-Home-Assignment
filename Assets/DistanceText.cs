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
        transform.position = mousePosition + _offsetFromMouse;
    }

    public void UpdateText(string newText)
    {
        _text.text = newText;
    }
}
