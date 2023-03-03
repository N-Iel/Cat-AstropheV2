using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script will manage all the states and acctions related with IA
/// </summary>
public class EnemyBrain : MonoBehaviour
{
    [SerializeField]
    List<Detector> detectors;

    [SerializeField]
    List<SteeringBehaviour> steeringBehaviours;

    [SerializeField]
    AIData aiData;

    [SerializeField]
    // Stable detection frequency prevents preformance issues
    float detectionDealy = 0.05f, aiUpdateDelay = 0.06f, attackDelay = 1f;

    [SerializeField]
    private float attackDistance = 0.5f;

    // Events
    public UnityEvent OnAttack;
    public UnityEvent<Vector2> OnMove;

    [SerializeField]
    Vector2 movementInput;

    [SerializeField]
    ContextSolver movementDirection;

    bool following = false;

    private void Start()
    {
        InvokeRepeating("PerformDetection", 0, detectionDealy);
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }

    private void Update()
    {
        //TODO CAMBIAR POR MAQUINA DE ESTADOS
        if (aiData.currentTarget && !following)
        {
            // Following Behaviour
            following = true;
            StartCoroutine(ChaseAndAttack());        
        }
        else if(aiData.GetTargetCount() > 0)
        {
            // If there is no target assigned but is detected we assign it
            aiData.currentTarget = aiData.targets[0];
        }
        OnMove?.Invoke(movementInput);
    }

    private IEnumerator ChaseAndAttack()
    {
        if (!aiData.currentTarget)
        {
            // Stop following
            Debug.Log(gameObject.name + " stopped following");
            movementInput = Vector2.zero;
            following = false;
            yield break;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

            if(distance < attackDistance)
            {
                // Attack Behaviour
                movementInput = Vector2.zero;
                OnAttack?.Invoke();
                yield return new WaitForSeconds(attackDelay);
            }
            else
            {
                // Keep Following
                movementInput = movementDirection.GetDirectionToMove(steeringBehaviours, aiData);
                yield return new WaitForSeconds(aiUpdateDelay);
            }
            StartCoroutine(ChaseAndAttack());
        }
    }
}
