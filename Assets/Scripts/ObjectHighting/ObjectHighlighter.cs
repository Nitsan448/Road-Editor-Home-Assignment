using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlighter
{
    private GameObject _previouslyHitObject;

    public void HandleObjectHighlighting(GameObject hitObject)
    {
        if (_previouslyHitObject == hitObject) return;
        HighlightableObject highlightableObject;

        if (hitObject != null && hitObject.TryGetComponent(out highlightableObject))
        {
            highlightableObject.Highlight();
        }

        if (_previouslyHitObject != null && _previouslyHitObject.TryGetComponent(out highlightableObject))
        {
            highlightableObject.StopHighlight();
        }
        _previouslyHitObject = hitObject;
    }
}
