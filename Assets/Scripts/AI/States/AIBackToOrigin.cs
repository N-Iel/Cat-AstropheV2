using Constants;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class AIBackToOrigin : State
{
    #region Base Params
    // State
    [field: Header("State")]
    [field: SerializeField]
    public override string stateName { get; set; }
    [field: SerializeField]
    public override List<States> triggerStates { get; set; }
    [field: SerializeField]
    public override List<States> stopStates { get; set; }
    public override bool isActive { get; set; }
    #endregion

    #region Custom Params

    #region Movement
    // Movemenet
    [field: Header("Movement")]

    [SerializeField]
    float distanceToTargetThreshold;

    [SerializeField]
    float movementDelay;
    #endregion

    #region Components
    [field: Header("Components")]

    [field: SerializeField]
    public Rigidbody2D rb { get; set; }

    [SerializeField]
    Transform characterTransform;

    [SerializeField]
    Transform origin;
    #endregion

    #region Events
    [Header("Events")]
    [field: SerializeField]
    UnityEvent<Vector2, Rigidbody2D> onRecover;

    [field: SerializeField]
    UnityEvent<Brain> onOriginReached;
    #endregion

    #endregion

    public override IEnumerator RunBehaviour(Brain originBrain, AIData aiData)
    {
        if (!isActive || !characterTransform || !origin) yield break;

        if (Vector2.Distance(characterTransform.position, origin.position) > distanceToTargetThreshold)
        {
            // KeepAttacking
            onRecover?.Invoke(origin.position, rb);
            yield return new WaitForSeconds(movementDelay);
        }
        else
        {
            rb.velocity = Vector2.zero;
            onOriginReached.Invoke(originBrain);
            // TEMPORAL
            originBrain.UpdateState(States.pasive);
            yield break;
        }
        StartCoroutine(RunBehaviour(originBrain, aiData));
    }
}
