using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCaster
{
    public Vector3 HitPositionOnTerrain { get; private set; }

    private Camera _mainCamera;
    private TerrainCollider _terrainCollider;
    private Ray _ray;

    public MouseRayCaster(TerrainCollider terrainCollider)
    {
        _mainCamera = Camera.main;
        _terrainCollider = terrainCollider;
    }

    public void Update()
    {
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (_terrainCollider.Raycast(_ray, out RaycastHit hitData, 1000))
        {
            HitPositionOnTerrain = hitData.point;
        }
    }

    public GameObject GetHitObject()
    {
        Physics.Raycast(_ray, out RaycastHit hitData, 1000);
        return hitData.transform.gameObject;
    }
}
