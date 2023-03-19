using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Move the character to the stored position trying to damage the target
/// </summary>
public class AIAttackingPosition : State
{
    #region Base Params
    // Base Params
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
    // Custo Params
    [field: Header("Custom Params")]

    [field: SerializeField]
    // Test random between range
    float ChasingTime = 5f;

    [field: Header("Character Components")]

    [field: SerializeField]
    Rigidbody2D characterRb;

    [field: Header("Events")]
    [field: SerializeField]
    UnityEvent onAttack;
    #endregion

    public override IEnumerator RunBehaviour(Brain originBrain, AIData aiData)
    {
        onAttack?.Invoke();

        yield return new WaitForSeconds(ChasingTime);
        originBrain.UpdateState(States.exhausted);
    }
}
