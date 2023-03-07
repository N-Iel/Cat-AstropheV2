using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAttackPreformed, OnAttackFinish;

    public void EnableAttack()
    {
        OnAttackPreformed?.Invoke();
    }

    public void DisableAttack()
    {
        OnAttackFinish?.Invoke();
    }
}
