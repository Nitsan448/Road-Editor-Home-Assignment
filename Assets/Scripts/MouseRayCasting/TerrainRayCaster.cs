using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRayCaster
{
    public Vector3 HitPositionOnTerrain { get; private set; }

    private TerrainCollider _terrainCollider;
    private Camera _mainCamera;
    private Ray _ray;

    public TerrainRayCaster(TerrainCollider terrainCollider, Camera mainCamera)
    {
        _terrainCollider = terrainCollider;
        _mainCamera = mainCamera;
    }

    public void Update()
    {
        FindHitPositionOnTerrain();
    }

    private void FindHitPositionOnTerrain()
    {
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (_terrainCollider.Raycast(_ray, out RaycastHit hitData, 1000))
        {
            HitPositionOnTerrain = hitData.point;
        }
    }
}
