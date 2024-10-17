using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCastsManager : MonoBehaviour
{
    public Vector3 HitPositionOnTerrain => _terrainRayCaster.HitPositionOnTerrain;
    public GameObject HitObject => _objectRayCaster.HitObject;

    private TerrainRayCaster _terrainRayCaster;
    private ObjectRayCaster _objectRayCaster;
    [SerializeField] private TerrainCollider _terrainCollider;

    private void Start()
    {
        Camera mainCamera = Camera.main;
        _terrainRayCaster = new TerrainRayCaster(_terrainCollider, mainCamera);
        _objectRayCaster = new ObjectRayCaster(mainCamera);
    }

    private void Update()
    {
        _terrainRayCaster.Update();
        _objectRayCaster.Update();
    }
}
