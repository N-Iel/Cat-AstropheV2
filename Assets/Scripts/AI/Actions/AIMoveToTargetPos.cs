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
    float speed = 10;
    [SerializeField]
    [Tooltip("Limit on how much speed can increase or decrease every time it applies")]
    float speedVariation = 0.2f;
    [SerializeField]
    Transform originPosition;

    [Header("Dynamic speed")]
    [SerializeField]
    bool dynamicSpeed = false;
    [SerializeField]
    [Tooltip("Ammount of speed reduce every second")]
    float ratePerSecond = 0.2f;
    Vector2 direction;
    float bufferSpeed;
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
    [SerializeField]
    float shadowMagnitud = 10f;
    Shadow shadow;
    public bool canMove { get; set; }
    #endregion

    #region Gizmos
    [field: Header("Debug")]
    [SerializeField]
    bool drawGizmos = false;
    Vector2 selectedDir = Vector2.zero;
    #endregion 

    private void Start()
    {
        shadow = GetComponent<Shadow>();
    }

    private void FixedUpdate()
    {
        if (!aiData.currentTarget || !canMove)
        {
            bufferSpeed = speed;
            return;
        }

        // Setting up the values
        direction = Utils.getDirection(aiData.currentTarget.position, originPosition.position);
        bufferSpeed -= Random.Range(ratePerSecond - speedVariation, ratePerSecond + speedVariation) * Time.deltaTime;
        if(!drawGizmos) Debug.Log(bufferSpeed);

        // Apply movement
        rb.AddForce(direction * (!dynamicSpeed ? bufferSpeed : Random.Range(bufferSpeed - speedVariation, bufferSpeed + speedVariation)));

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