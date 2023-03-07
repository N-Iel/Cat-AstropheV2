using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script requires an enemy with the full context seeking setUp
/// </summary>
public class AIContextSteeringDetector : State
{
    [Header("Detector")]
    public float detectionDealy = 0.05f;

    [SerializeField]
    List<Detector> detectors;

    [SerializeField]
    List<SteeringBehaviour> steeringBehaviours;

    [field: Header("State Props")]
    [field: SerializeField]
    public override string stateName { get; set; }
    [field: SerializeField]
    public override string triggerState { get; set; }
    [field: SerializeField]
    public override string stopState { get; set; }
    public override bool isActive { get; set; }


    public override IEnumerator RunBehaviour(Brain originBrain, AIData aiData)
    {
        isActive = true;

        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }

        if (aiData.currentTarget)
        {
            // Following Behaviour
            originBrain.UpdateState("agresive");
        }
        else if (aiData.GetTargetCount() > 0)
        {
            // If there is no target assigned but is detected we assign it
            aiData.currentTarget = aiData.targets[0];
        }

        yield return new WaitForSeconds(detectionDealy);
        StartCoroutine(RunBehaviour(originBrain, aiData));
    }
}
