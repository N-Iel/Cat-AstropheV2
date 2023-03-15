using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // Vector Things
    public static Vector2 getDirection(Vector2 targetPos, Vector2 originPos)
    {
        Vector2 heading = targetPos - originPos;
        return (heading / heading.magnitude).normalized;
    }
}
