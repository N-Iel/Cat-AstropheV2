using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextSolver : MonoBehaviour
{
    [SerializeField]
    bool showGizmos = true;

    // Gizmos params
    float[] interestGizmo = new float[0];
    Vector2 result = Vector2.zero;
    float rayLength = 1;

    private void Start()
    {
        interestGizmo= new float[8];
    }

    public Vector2 GetToMove(List<SteeringBehaviour> behaviours, AIData aiData)
    {
        float[] danger = new float[8];
        float[] interest = new float[8];

        // Get all the info from behaviours
        foreach (SteeringBehaviour behaviour in behaviours)
        {
            (danger, interest) = behaviour.GetSteering(danger, interest, aiData);
        }

        // Compensate interest s with their danger value
        for (int i = 0; i < 8; i++)
        {
            interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
        }

        interestGizmo = interest;

        // Calculate  (for smoother movement)
        Vector2 output = Vector2.zero;
        for (int i = 0; i < 8; i++)
        {
            output += s.eights[i] * interest[i];
        }
        output.Normalize();

        result = output;

        // Return the calculated 
        return result;
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying && showGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, result * rayLength);
        }
    }
}
