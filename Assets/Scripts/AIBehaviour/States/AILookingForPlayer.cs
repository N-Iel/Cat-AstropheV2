using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

/// <summary>
/// State that check for players info and triggers agresive states
/// </summary>
public class AILookingForPlayer : State
{
    #region Base Variables
    // Base Params
    [field: SerializeField]
    public override string stateName { get; set; }      // State of the name (debug)

    [field: SerializeField]
    public override States triggerState { get; set; }   // State that will make this script run

    [field: SerializeField]
    public override States stopState { get; set; }      // State that will make this script stop
    #endregion

    #region Custom Variables
    // Custom Params
    [field: SerializeField]
    public float delay { get; set; }                    // Delay between operations (optimization)

    [field: SerializeField]
    public Detector detector { get; set; }              // Detector used for this state

    [field: SerializeField]
    [Tooltip("This will prevent the state from beeing deactivated")]
    public bool isContinuous { get; set; }

    // NonSerialized Variables
    public override bool isActive { get; set; }
    #endregion

    public override IEnumerator RunBehaviour(Brain originBrain, AIData aiData)
    {
        isActive = true;

        detector.Detect(aiData);

        if (aiData.targetPosition || aiData.currentTarget)
        {
            Debug.Log("Target Detected");
            isActive = isContinuous;
         
            // Following Behaviour
            originBrain.UpdateState(States.agresive);
            yield break;
        }

        yield return new WaitForSeconds(delay);
        StartCoroutine(RunBehaviour(originBrain, aiData));
    }
}
