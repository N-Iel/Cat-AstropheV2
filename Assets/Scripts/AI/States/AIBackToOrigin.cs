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
    public override IEnumerator corutine { get; set; }

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

    [Header("Others")]
    [SerializeField]
    bool resetTargetOnOrigin = true;

    #region Events
    [Header("Events")]
    [field: SerializeField]
    UnityEvent onRecover;

    [field: SerializeField]
    UnityEvent<Brain> onOriginReached;

    [field: SerializeField]
    public override UnityEvent onCorutineStop { get; set; }
    #endregion

    #endregion

    public override IEnumerator RunBehaviour(Brain originBrain, AIData aiData)
    {
        if (!characterTransform || !origin) yield break;
        // Update target
        aiData.currentTarget = origin;

        // Start process
        onRecover?.Invoke();

        // Wait until origin is reached
        yield return new WaitWhile(() => Vector2.Distance(characterTransform.position, aiData.currentTarget.position) > distanceToTargetThreshold);

        // Finish
        if (resetTargetOnOrigin) aiData.currentTarget = null;
        onOriginReached.Invoke(originBrain);
    }
}
