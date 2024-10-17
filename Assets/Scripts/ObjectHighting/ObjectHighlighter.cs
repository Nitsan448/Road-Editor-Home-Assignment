using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlighter
{
    private GameObject _previouslyHitObjectParent;

    public void HandleObjectHighlighting(GameObject hitObject)
    {
        if (_previouslyHitObjectParent == hitObject) return;
        HighlightableObject highlightableObject;

        if (_previouslyHitObjectParent != null && _previouslyHitObjectParent.TryGetComponent(out highlightableObject))
        {
            highlightableObject.StopHighlight();
        }

        if (hitObject == null)
        {
            _previouslyHitObjectParent = null;
            return;
        }

        GameObject hitObjectParent = hitObject.transform.parent.gameObject;
        _previouslyHitObjectParent = hitObjectParent;
        if (hitObjectParent.TryGetComponent(out highlightableObject))
        {
            highlightableObject.Highlight();
        }

    }
}
