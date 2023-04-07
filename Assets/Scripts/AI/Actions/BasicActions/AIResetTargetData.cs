using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIResetTargetData : MonoBehaviour
{
    [SerializeField]
    AIData aiData;

    public void ResetTarget()
    {
        aiData.currentTarget = null;
        aiData.detectedPos = Vector2.zero;
    }
}
