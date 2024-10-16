using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCaster
{
    private Camera _mainCamera;
    public GameObject HitObject { get; private set; }
    public Vector3 HitPosition { get; private set; }

    public MouseRayCaster()
    {
        _mainCamera = Camera.main;
    }

    public void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitData, 1000);
        HitPosition = hitData.point;
        HitObject = hitData.transform.gameObject;
    }
}
