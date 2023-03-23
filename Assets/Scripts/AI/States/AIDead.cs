using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIDead : State
{
    #region State Params
    [field: Header("State")]
    [field: SerializeField]
    public override string stateName { get; set; }
    [field: SerializeField]
    public override List<States> triggerStates { get; set; }
    [field: SerializeField]
    public override List<States> stopStates { get; set; }
    public override IEnumerator corutine { get; set; }
    public override UnityEvent onCorutineStop { get; set; }

    #endregion

    #region Custom Params
    [Header("Custom params")]
    [SerializeField]
    float delay = 0f;
    #endregion

    #region Events
    [Header("Events")]
    [SerializeField]
    UnityEvent onDead;
    #endregion

    public override IEnumerator RunBehaviour(Brain originBrain, AIData aiData)
    {
        yield return new WaitForSeconds(delay);
        onDead.Invoke();
    }
}
