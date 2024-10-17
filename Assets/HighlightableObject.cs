using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightableObject : MonoBehaviour
{
    [SerializeField] private Color _highlightColor;
    [SerializeField] private float _highlightScaleFactor;
    [SerializeField] private MeshRenderer _meshRenderer;

    private Color _defaultColor;
    private bool _highlighted;

    private void Start()
    {
        _defaultColor = _meshRenderer.material.color;
    }

    public void Highlight()
    {
        if (_highlighted) return;
        _highlighted = true;
        _meshRenderer.material.color = _highlightColor;
        transform.localScale *= _highlightScaleFactor;
    }

    public void StopHighlight()
    {
        if (!_highlighted) return;
        _highlighted = false;
        _meshRenderer.material.color = _defaultColor;
        transform.localScale /= _highlightScaleFactor;
    }
}
