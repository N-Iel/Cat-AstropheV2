using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Move the character to the stored position trying to damage the target
/// </summary>
public class AIBasicAttack : State
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
    public override IEnumerator corutine { get; set; }


    #endregion

    #region Custom Params
    // Custo Params
    [field: Header("Custom Params")]

    [field: SerializeField]
    // Test random between range
    float attackTimeLimit = 5f;

    [field: Header("Character Components")]

    [field: Header("Events")]
    [field: SerializeField]
    UnityEvent onAttack;
    [field: SerializeField]
    UnityEvent<Brain> onFinish;
    [field: SerializeField]
    public override UnityEvent onCorutineStop { get; set; }
    #endregion
    public override IEnumerator RunBehaviour(Brain originBrain, AIData aiData)
    {
        onAttack?.Invoke();

        yield return new WaitForSeconds(attackTimeLimit);
        onFinish?.Invoke(originBrain);
    }
}
