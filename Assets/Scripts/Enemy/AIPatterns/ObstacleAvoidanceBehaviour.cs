using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float radius = 2f,               // Raidus of detection for dangerously close obstacles
                  agentColliderSize = 0.6f;  // Mandatory avoidance obstacle Range (this is equivalent to the enemy collider size)

    [SerializeField]
    private bool showGizmo = true;

    float[] dangersResultTemp = null;        // Gizmo Params

    /// <summary>
    /// Apunte: Este método de aquí abajo es de tipo Anónimo, esto permite crear diferentes elementos sin tener que generar su estructura típica
    /// En este caso un método que devolverá dos float[] y con una funcionalidad interna, sin necesidad de declarar una clase, estructura o incluso métodos al rededor.
    /// Las prioridades se miden del (min)0 - (max)1 en función de la cercanía al obstáculo
    /// </summary>
    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        foreach (Collider2D obstacleCollider in aiData.obstacles)
        {
            // From the data stored in AIData extract the distance to the detected obstacles
            Vector2 directionToObstacle = obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distanceToObstacle = directionToObstacle.magnitude;

            // Calculate weight based on the distance to the obstacle
            float weight
                = distanceToObstacle <= agentColliderSize
                ? 1
                : (radius - distanceToObstacle) / radius;

            Vector2 directionToObstacleNormalized = directionToObstacle.normalized;

            for(int i = 0; i < Directions.eightDirections.Count; i++)
            {
                // We calculate how optimal is every direcction in order to get close to the obstacle (in a bad way)
                float result = Vector2.Dot(directionToObstacleNormalized, Directions.eightDirections[i]);

                float valueToPutIn = result * weight;

                // In case we where sourrounded by multiple obstacles we keep the riskier ones
                if(valueToPutIn > danger[i])
                {
                    danger[i] = valueToPutIn;
                }

            }
        }
        dangersResultTemp = danger;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {
        if(showGizmo == false)
            return;

        // Will draw red lines with lengths equivalent to the threat that they respresent arround the player
        if(Application.isPlaying && dangersResultTemp != null)
        {
            //TODO Here the is another dangersResult check but i keep it out to check if it's needed
            Gizmos.color = Color.red;
            for (int i = 0; i < dangersResultTemp.Length; i++)
            {
                Gizmos.DrawRay(
                    transform.position,
                    Directions.eightDirections[i] * dangersResultTemp[i]
                    );
            }
        }
        else
        {
            // If there is no threat, the detection area is drawed
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}

public static class Directions
{
    public static List<Vector2> eightDirections = new List<Vector2>()
    {
        new Vector2(0, 1).normalized,
        new Vector2(1, 1).normalized,
        new Vector2(1, 0).normalized,
        new Vector2(1, -1).normalized,
        new Vector2(0, -1).normalized,
        new Vector2(-1, -1).normalized,
        new Vector2(-1, 0).normalized,
        new Vector2(-1, 1).normalized,
    };
}
