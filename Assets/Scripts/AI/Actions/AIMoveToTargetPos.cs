using System.Collections;
using System.Collections.Generic;
using Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

/// <summary>
/// Move to a position while detect player collissions in order to cause Dmg
/// </summary>
public class AIMoveToTargetPos : MonoBehaviour
{
    #region Movement
    [Header("Movement")]
    [SerializeField]
    float timeToReachTarget;
    [SerializeField]
    float speed;
    [SerializeField]
    Transform originPosition;
    Vector2 direction;
    #endregion

    #region Others
    [Header("Components")]
    [SerializeField]
    AIData aiData;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    [Tooltip("Optional parameter, used for rotation or flip")]
    EnemyAnimator animator;

    [Header("Others")]
    [SerializeField]
    bool rotateSprite = false;
    public bool canMove { get; set; }

    Shadow shadow;
    #endregion

    #region Gizmos
    [field: Header("Debug")]
    [SerializeField]
    bool drawGizmos = false;

    [SerializeField]
    float shadowMagnitud = 10f;

    Vector2 selectedDir = Vector2.zero;
    #endregion 

    private void Start()
    {
        shadow = GetComponent<Shadow>();
    }

    private void FixedUpdate()
    {
        if (!aiData.currentTarget || !canMove) return;

        // Setting up the values
        direction = Utils.getDirection(aiData.currentTarget.position, originPosition.position);

        // Apply movement
        rb.AddForce(direction * speed);

        // Extra
        if (rotateSprite && animator) animator.RotatoToLookingDir(direction);

        // Shadow
        if (shadow) shadow.enabled = rb.velocity.magnitude > shadowMagnitud;

        // Debug
        selectedDir = direction;
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos || selectedDir == Vector2.zero) return;

        Debug.DrawRay(
            !originPosition
                ? (Vector2)transform.position 
                : (Vector2)originPosition?.position
            , selectedDir
            , Color.red);
    }
}