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
public class AIMoveToTargetPos : MonoBehaviour, Movement
{
    #region Movement
    [field: Header("Movement")]
    [field: SerializeField]
    public float timeToReachTarget { get; set; }
    [field: SerializeField]
    public float maxSpeed { get; set; }
    [field: SerializeField]
    public Rigidbody2D rb { get; set; }
    public float force { get; set; }
    public float distance { get; set; }
    public Vector2 direction { get; set; }
    #endregion

    #region Custom Params
    [field: SerializeField]
    AIData aiData;

    [field: SerializeField]
    Transform originPosition;
    Vector2 speed;
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
    }

    private void FixedUpdate()
    {
        // Setting up the values
        direction = Utils.getDirection(aiData.currentTarget.position, originPosition.position);
        distance = Vector2.Distance(aiData.currentTarget.position, originPosition.position);
        speed = Vector2.SmoothDamp(originPosition.position, aiData.currentTarget.position, ref speed, timeToReachTarget, maxSpeed);

        // Apply movement
        rb.AddForce(direction.normalized * speed.magnitude);

        // Debug
        selectedDir = direction;
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