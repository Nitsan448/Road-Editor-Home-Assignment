using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlighter
{
    public GameObject previouslyHitObject;

    public void HandleObjectHighlighting(GameObject hitObject)
    {
        HighlightableObject highlightableObject;
        if (previouslyHitObject != hitObject) return;

        if (hitObject != null && hitObject.TryGetComponent(out highlightableObject))
        {
            highlightableObject.Highlight();
        }

        if (previouslyHitObject != null && hitObject.TryGetComponent(out highlightableObject))
        {
            highlightableObject.StopHighlight();
        }
        previouslyHitObject = hitObject;
    }
}
