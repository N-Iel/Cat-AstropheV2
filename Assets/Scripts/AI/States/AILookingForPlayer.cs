using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// State that check for players info and triggers aggressive states
/// </summary>
public class AILookingForPlayer : State
{
    #region Base Variables
    // Base Params
    [field: Header("State")]
    [field: SerializeField]
    public override string stateName { get; set; }      // State of the name (debug)

    [field: SerializeField]
    public override List<States> triggerStates { get; set; }   // State that will make this script run

    [field: SerializeField]
    public override List<States> stopStates { get; set; }      // State that will make this script stop
    public override bool isActive { get; set; }
    #endregion

    #region Custom Variables
    // Custom Params
    [field: Header("Custom Params")]
    [field: SerializeField]
    public float delay { get; set; }                    // Delay between operations (optimization)

    [field: SerializeField]
    [Tooltip("This will prevent the state from beeing deactivated")]
    public bool isContinuous { get; set; }

    [field: Header("Events")]
    [field: SerializeField]
    UnityEvent<AIData> onDetect;   
    #endregion

    public override IEnumerator RunBehaviour(Brain originBrain, AIData aiData)
    {
        if (!isActive) yield break;

        onDetect.Invoke(aiData);

        if (aiData.targetPosition != Vector2.zero || aiData.currentTarget != null)
        {
            Debug.Log("Target Detected");        
            
            // Following Behaviour
            originBrain.UpdateState(States.aggressive);

            if (!isContinuous) yield break;
        }

        yield return new WaitForSeconds(delay);
        StartCoroutine(RunBehaviour(originBrain, aiData));
    }
}
