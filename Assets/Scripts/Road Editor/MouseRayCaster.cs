using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: rename and split
public class MouseRayCaster : MonoBehaviour
{
    public Vector3 HitPositionOnTerrain { get; private set; }
    public GameObject HitObject { get; private set; }

    [SerializeField] private TerrainCollider _terrainCollider;
    private Camera _mainCamera;
    private Ray _ray;
    private ObjectHighlighter _objectHighlighter;

    private void Start()
    {
        _objectHighlighter = new ObjectHighlighter();
        _mainCamera = Camera.main;
    }

    public void Update()
    {
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        FindHitPositionOnTerrain();
        FindHitObject();
        _objectHighlighter.HandleObjectHighlighting(HitObject);
    }

    private void FindHitPositionOnTerrain()
    {
        if (_terrainCollider.Raycast(_ray, out RaycastHit hitData, 1000))
        {
            HitPositionOnTerrain = hitData.point;
        }
    }

    public void FindHitObject()
    {
        Physics.Raycast(_ray, out RaycastHit hitData, 1000);
        HitObject = hitData.transform.gameObject;
    }

}
