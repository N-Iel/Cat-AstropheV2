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

    EnemyAnimator animator;
    Rigidbody2D rb;

    private void Awake()
    {
        animator = model.GetComponent<EnemyAnimator>();
        rb = model.GetComponent<Rigidbody2D>();
    }

    public void StartNestting()
    {
        animator.PlayAnimation(newAnimations.reset);
        model.transform.localPosition = Vector3.zero;
        rb.velocity = Vector2.zero;

        Invoke("UpdateState", 0.12f);
    }

    void UpdateState()
    {
        brain.UpdateState(States.pasive);
        this.enabled = false;
    }
}
