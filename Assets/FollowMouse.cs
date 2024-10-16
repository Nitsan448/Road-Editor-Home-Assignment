using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    public Vector3 HitPositionOnTerrain { get; private set; }

    [SerializeField] private TerrainCollider _terrainCollider;
    private Ray _ray;

    public void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (_terrainCollider.Raycast(_ray, out RaycastHit hitData, 1000))
        {
            HitPositionOnTerrain = hitData.point;
        }
        transform.position = HitPositionOnTerrain + _offset;
    }

}
