using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script exclusive for Owl focused on the final state when it reaches the tree
/// </summary>
public class BackToNest : MonoBehaviour
{
    [SerializeField]
    Brain brain;

    [SerializeField]
    GameObject model;

    [SerializeField]
    EnemyAnimator animator;

    private void OnEnable()
    {
        animator.PlayAnimation(newAnimations.reset);
        model.transform.localPosition = Vector3.zero;
        Invoke("UpdateState", 0.12f);
    }

    void UpdateState()
    {
        brain.UpdateState(States.pasive);
        this.enabled = false;
    }
}
