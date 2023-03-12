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
    public float acceleration { get; set; }
    [field: SerializeField]
    public float maxSpeed { get; set; }
    [field: SerializeField]
    public float decceleration { get; set; }
    public float speed { get; set; }
    public Rigidbody2D rb { get; set; }
    public Vector2 direction { get; set; }

    [field: SerializeField]
    Transform originPosition;
    #endregion

    #region Gizmos
    [field: Header("Debug")]
    [SerializeField]
    bool drawGizmos = false;
    Vector2 selectedDir = Vector2.zero;
    #endregion

    public void ExecuteMovement(Vector2 targetPosition, Rigidbody2D characterRb)
    {
        direction = getToMove(targetPosition) ;
        rb = characterRb;
        speed = 0;

        MainMovement.ApplyMovement(this);
    }

    Vector2 getToMove(Vector2 targetPos)
    {
        Vector2 heading = targetPos - (!originPosition
                                        ? (Vector2)transform.position 
                                        : (Vector2)originPosition?.position);
        selectedDir = (heading / heading.magnitude).normalized;
        return selectedDir;
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
