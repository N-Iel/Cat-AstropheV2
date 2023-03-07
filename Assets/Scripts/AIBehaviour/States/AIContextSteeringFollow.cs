using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    ContextSolver movementDirection;

    [SerializeField]
    List<SteeringBehaviour> steeringBehaviours;

    [field: Header("State Props")]
    [field: SerializeField]
    public override string stateName { get; set ;}
    [field: SerializeField]
    public override string triggerState { get; set ;}
    [field: SerializeField]
    public override string stopState { get; set ;}
    [field: SerializeField]
    bool showGizmos { get; set; }
    public override bool isActive { get; set ;}

    [Header("Events")]
    public UnityEvent OnAttack;
    public UnityEvent<Vector2> OnMove;

    public override IEnumerator RunBehaviour(Brain originBrain, AIData aiData)
    {
        isActive = true;

        // Following script content 
        if (aiData.currentTarget == null)
        {
            // Stop following
            Debug.Log(gameObject.name + " stopped following");
            movementInput = Vector2.zero;
            isActive = false;
            yield break;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);
            Debug.Log(distance);
            if (distance < attackDistance)
            {
                // Attack Behaviour
                movementInput = Vector2.zero;
                Debug.Log("Attacking");
                OnAttack?.Invoke();
                yield return new WaitForSeconds(attackDelay);
            }
            else
            {
                // Keep Following
                movementInput = movementDirection.GetDirectionToMove(steeringBehaviours, aiData);
                Debug.Log("Following");
                OnMove?.Invoke(movementInput);
                yield return new WaitForSeconds(followDealy);
            }
        }

        StartCoroutine(RunBehaviour(originBrain, aiData));
    }

    // It will generate dots on the detected obstacles for debug
    private void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Debug.DrawRay(transform.position, Vector2.right * attackDistance, Color.magenta);
    }
}
