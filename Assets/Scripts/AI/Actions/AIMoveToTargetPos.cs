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
    float maxSpeed;
    [SerializeField]
    Transform originPosition;
    Vector2 direction, speed;
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

    [SerializeField]
    [Tooltip("Optional parameter, it will update the target onEnabled")]
    Transform newTarget;

    [Header("Others")]
    [SerializeField]
    bool rotateSprite = false;
    #endregion

    #region Gizmos
    [field: Header("Debug")]
    [SerializeField]
    bool drawGizmos = false;
    Vector2 selectedDir = Vector2.zero;
    #endregion

    private void OnEnable()
    {
        speed = Vector2.zero;

        if(newTarget) aiData.currentTarget = newTarget;
    }

    private void FixedUpdate()
    {
        if (!aiData.currentTarget) return;

        // Setting up the values
        direction = Utils.getDirection(aiData.currentTarget.position, originPosition.position);
        speed = Vector2.SmoothDamp(originPosition.position, aiData.currentTarget.position, ref speed, timeToReachTarget, maxSpeed);

        // Apply movement
        rb.AddForce(direction * speed.magnitude);

        // Extra
        if (rotateSprite && animator) animator.RotatoToLookingDir(direction);

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