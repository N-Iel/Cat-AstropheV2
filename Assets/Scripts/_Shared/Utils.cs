using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // Vector Things
    public static Vector2 getDirection(Vector2 targetPos, Vector2 originPos)
    {
        Vector2 heading = targetPos - originPos;
        return heading.normalized;
    }

    // Time things
    public static void UpdateTimeScale(float _timeScale)
    {
        Time.timeScale = _timeScale;
    }
}
