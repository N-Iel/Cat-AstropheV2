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
    float distanceToTargetThreshold = 0.5f;

    [field: SerializeField]
    float delay = 0.5f;

    [field: Header("Character Components")]
    [field: SerializeField]
    Transform characterTransform;

    [field: SerializeField]
    Rigidbody2D characterRb;

    [field: Header("Events")]
    [field: SerializeField]
    UnityEvent<Vector2, Rigidbody2D> onAttack;
    [field: SerializeField]
    UnityEvent onTargetReached;
    #endregion

    public override IEnumerator RunBehaviour(Brain originBrain, AIData aiData)
    {
        onAttack?.Invoke(aiData.targetPosition, characterRb);
        yield return new WaitUntil(() => Vector2.Distance(characterTransform.position, aiData.targetPosition) > distanceToTargetThreshold);

        //onTargetReached?.Invoke();
        //aiData.targetPosition = Vector2.zero;
        //yield return new WaitForSeconds(delay);

        //originBrain.UpdateState(States.exhausted);
        yield break;
    }
}
