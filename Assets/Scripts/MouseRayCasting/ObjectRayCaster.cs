using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRayCaster
{
    public GameObject HitObject { get; private set; }
    
    private Camera _mainCamera;
    private Ray _ray;
    private ObjectHighlighter _objectHighlighter;

    public ObjectRayCaster(Camera mainCamera)
    {
        _mainCamera = mainCamera;
        _objectHighlighter = new ObjectHighlighter();
    }

    public void Update()
    {
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        FindHitObject();
        _objectHighlighter.HandleObjectHighlighting(HitObject);
    }

    private void FindHitObject()
    {
        if (Physics.Raycast(_ray, out RaycastHit hitData, 1000))
        {
            HitObject = hitData.transform.gameObject;
            return;
        }

        HitObject = null;
    }
}
