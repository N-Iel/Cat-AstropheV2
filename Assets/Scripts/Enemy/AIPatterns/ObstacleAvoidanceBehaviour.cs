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
    /// Apunte: Este m�todo de aqu� abajo es de tipo An�nimo, esto permite crear diferentes elementos sin tener que generar su estructura t�pica
    /// En este caso un m�todo que devolver� dos float[] y con una funcionalidad interna, sin necesidad de declarar una clase, estructura o incluso m�todos al rededor.
    /// Las prioridades se miden del (min)0 - (max)1 en funci�n de la cercan�a al obst�culo
    /// </summary>
    //public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    //{
    //    foreach (Collider2D obstacleCollider in aiData.obstacles)
    //    {
    //        // From the data stored in AIData extract the distance to the detected obstacles
    //        Vector2 directionToObstacle = obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
    //        float distanceToObstacle = directionToObstacle.magnitude;

    //        // Calculate priority based on the distance to the obstacle
    //        float priority
    //            = distanceToObstacle <= agentColliderSize
    //            ? 1
    //            : (radius - distanceToObstacle) / radius;

    //        Vector2 directionToObstacleNormalized = directionToObstacle.normalized;


    //    }
    //}

}
