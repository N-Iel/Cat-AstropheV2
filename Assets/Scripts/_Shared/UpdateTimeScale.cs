using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTimeScale : MonoBehaviour
{
    public static void setTimeScale(float _timeScale)
    {
        Time.timeScale = _timeScale;
    }
}
