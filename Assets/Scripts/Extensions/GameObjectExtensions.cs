using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    public static bool HasParent(this GameObject gameObject)
    {
        return gameObject != null && gameObject.transform.parent != null;
    }

}
