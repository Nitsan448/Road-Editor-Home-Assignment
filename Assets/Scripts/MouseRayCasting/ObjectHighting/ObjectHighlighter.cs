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

        if (_previouslyHitObject.HasParent() && _previouslyHitObject.transform.parent.TryGetComponent(out highlightableObject))
        {
            highlightableObject.StopHighlight();
        }

        if (hitObject == null)
        {
            _previouslyHitObject = null;
            return;
        }

        GameObject hitObjectParent = hitObject.transform.parent.gameObject;
        _previouslyHitObject = hitObject;
        if (hitObjectParent.TryGetComponent(out highlightableObject))
        {
            highlightableObject.Highlight();
        }

    }
}
