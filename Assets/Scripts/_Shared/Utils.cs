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

    // Check wallClipping
    public static bool isOnWall(Vector2 _pos)
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(_pos, 0.1f))
        {
            if (collider.CompareTag("Ground"))
                return true;
        }
        return false;
    }

    // Change state of gameObjects on a list
    public static List<GameObject> updateListItems(List<GameObject> _list, bool isActive)
    {
        foreach (GameObject item in _list)
            item.SetActive(isActive);

        return _list;
    }
}
