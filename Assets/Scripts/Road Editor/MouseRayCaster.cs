using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: rename and split
public class MouseRayCaster : MonoBehaviour
{
    public Vector3 HitPositionOnTerrain { get; private set; }

    [SerializeField] private TerrainCollider _terrainCollider;
    private Camera _mainCamera;
    private Ray _ray;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void Update()
    {
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (_terrainCollider.Raycast(_ray, out RaycastHit hitData, 1000))
        {
            HitPositionOnTerrain = hitData.point;
        }
    }

    //TODO: color and scale highlighted objects
    public GameObject GetHitObject()
    {
        Physics.Raycast(_ray, out RaycastHit hitData, 1000);
        return hitData.transform.gameObject;
    }
}
