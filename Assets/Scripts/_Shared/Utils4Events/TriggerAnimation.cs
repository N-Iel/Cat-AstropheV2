using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField]
    EnemyAnimator animator;

    [SerializeField]
    newAnimations targetAnimation;

    public void nextAnimation()
    {
        animator.PlayAnimation(targetAnimation);
    }
}
