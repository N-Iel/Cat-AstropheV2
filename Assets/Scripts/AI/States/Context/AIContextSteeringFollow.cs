using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using UnityEngine.Events;

/// <summary>
/// This script requires an enemy with the full context seeking setUp
/// </summary>
public class AIContextSteeringFollow : State
{
    [Header("Parameters")]
    public float followDealy = 0.05f, attackDelay = 0.1f, attackDistance = 0.1f;
    Vector2 movementInput;

    [SerializeField]
    ContextSolver movement;

    [field: Header("Detector")]
    [SerializeField]
    public float delay { get; set; }

    [SerializeField]
    List<Detector> detectors;

    [SerializeField]
    List<SteeringBehaviour> steeringBehaviours;

    [field: Header("State Props")]
    [field: SerializeField]
    public override string stateName { get; set ;}
    [field: SerializeField]
    public override List<States> triggerStates { get; set ;}
    [field: SerializeField]
    public override List<States> stopStates { get; set ;}
    [field: SerializeField]
    public override IEnumerator corutine { get; set; }
    public override UnityEvent onCorutineStop { get; set; }


    [Header("Events")]
    public UnityEvent<Vector2> OnMove;

    public override IEnumerator RunBehaviour(Brain originBrain, AIData aiData)
    {
        do
        {
            foreach (Detector detector in detectors)
            {
                detector.Detect(aiData);
            }

            movementInput = movement.GetToMove(steeringBehaviours, aiData);
            OnMove?.Invoke(movementInput);
            yield return new WaitForSeconds(followDealy);
        } while (true);
    }
}
