using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetector : Detector
{
    [SerializeField]
    private float detectionRadius = 2.0f; 

    [SerializeField]
    private LayerMask layerMask;         // Obstacle LayerMask

    [SerializeField]
    private bool showGizmos;             // RealTime debug feedback of detecctions

    Collider2D[] colliders;              // Obstacle colliders

    // It will check for obstacles in range and store their colliders
    public override void Detect(AIData aiData)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, layerMask);
        aiData.obstacles = colliders;
    }

    // It will generate dots on the detected obstacles for debug
    private void OnDrawGizmos()
    {
        if (!showGizmos) return;

        if(Application.isPlaying && colliders != null)
        {
            Gizmos.color = Color.red;
            foreach (Collider2D obstacleCollider in colliders)
            {
                Gizmos.DrawSphere(obstacleCollider.transform.position, 0.2f);
            }
        }
    }
}
