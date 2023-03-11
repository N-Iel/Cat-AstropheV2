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
    public override States triggerState { get; set; }
    [field: SerializeField]
    public override States stopState { get; set; }
    public override bool isActive { get; set; }
    #endregion

    #region Custom Params
    // Custo Params
    [field: Header("Custom Params")]
    [field: SerializeField]
    float attackDelay = 0.5f;

    [field: SerializeField]
    float distanceToTargetThreshold = 0.5f;

    [field: Header("Character Components")]
    [field: SerializeField]
    Transform characterTransform;

    [field: SerializeField]
    Rigidbody2D characterRb;

    [field: Header("Events")]
    [field: SerializeField]
    UnityEvent<Vector2, Rigidbody2D> onAttack;
    #endregion

    public override IEnumerator RunBehaviour(Brain originBrain, AIData aiData)
    {
        if(Vector2.Distance(characterTransform.position, aiData.targetPosition) > distanceToTargetThreshold)
        {
            // KeepAttacking
            onAttack?.Invoke(aiData.targetPosition, characterRb);
            yield return new WaitForSeconds(attackDelay);
        }
        else
        {
            aiData.targetPosition = Vector2.zero;
            characterRb.velocity = Vector2.zero;
            originBrain.UpdateState(States.exhausted);
            yield break;
        }
        StartCoroutine(RunBehaviour(originBrain, aiData));
    }
}
