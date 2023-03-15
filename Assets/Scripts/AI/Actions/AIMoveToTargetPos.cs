using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

/// <summary>
/// Move to a position while detect player collissions in order to cause Dmg
/// </summary>
public class AIMoveToTargetPos : MonoBehaviour, Movement
{
    #region Movement
    [field: Header("Movement")]
    [field: SerializeField]
    public float timeToReachTarget { get; set; }
    [field: SerializeField]
    public float maxSpeed { get; set; }
    public float force { get; set; }
    public float distance { get; set; }
    public Rigidbody2D rb { get; set; }
    public Vector2 direction { get; set; }
    #endregion

    #region Custom Params
    [field: SerializeField]
    Transform originPosition;
    Vector2 targetPos;
    public bool canMove { get; set; }

    Vector2 speed = Vector2.zero;
    #endregion

    #region Gizmos
    [field: Header("Debug")]
    [SerializeField]
    bool drawGizmos = false;
    Vector2 selectedDir = Vector2.zero;
    #endregion

    public void ExecuteMovement(Vector2 targetPosition, Rigidbody2D characterRb)
    {
        targetPos = targetPosition;
        rb = characterRb;
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        direction = Utils.getDirection(targetPos, originPosition.position);
        selectedDir = direction;
        distance = Vector2.Distance(targetPos, originPosition.position);
        speed = Vector2.SmoothDamp(originPosition.position, targetPos, ref speed, timeToReachTarget, maxSpeed);
        rb.AddRelativeForce(direction.normalized * maxSpeed * Time.deltaTime);
        //MainMovement.ApplyMovement(this);
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