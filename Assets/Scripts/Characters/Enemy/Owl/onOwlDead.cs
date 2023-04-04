using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onOwlDead : MonoBehaviour
{
    [SerializeField]
    float postMortenDelay = 1.0f;

    [SerializeField]
    float rotationForce = 1.0f;

    [Header("Components")]
    [SerializeField]
    GameObject model;

    [SerializeField]
    EnemyAnimator animator;

    [SerializeField]
    Rigidbody2D rb;
    //public void Start()
    //{
    //    // TODO Preguntar porque esto no va
    //    //animator = model.GetComponent<EnemyAnimator>();
    //    //rb = model.GetComponent<Rigidbody2D>();
    //}

    public void onDead()
    {
        animator.PlayAnimation(newAnimations.dead);
        rb.AddTorque(rotationForce, ForceMode2D.Impulse);
        Invoke("PostMorten", postMortenDelay);
    }

    private void PostMorten()
    {
        Debug.Log("postmorten");
        rb.freezeRotation = true;
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;

        this.enabled = false;
        model.transform.parent.gameObject.SetActive(false);
    }
}
