using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField]
    private float targetDetectionRange = 5.0f;

    [SerializeField]
    private LayerMask targetLayerMask, obstacleLayerMask; // target, obstacle LayerMask

    [SerializeField]
    private bool showGizmos = false;                      // RealTime debug feedback of detecctions

    private List<Transform> colliders;                    // List of detected targets
    public override void Detect(AIData aiData)
    {
        // Check for target in range
        Collider2D targetCollider = Physics2D.OverlapCircle(transform.position, targetDetectionRange, targetLayerMask);

        if (targetCollider)
        {
            // Check if target is at sight
            Vector2 direction = (targetCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, targetDetectionRange, obstacleLayerMask);

            // Make sure that the collider detected is on the Target Layer
            if(hit.collider && (targetLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                Debug.DrawRay(transform.position, direction * targetDetectionRange, Color.magenta);
                colliders = new List<Transform>() { targetCollider.transform };
            }
            else
            {
                // Target out of sight
                colliders = null;
            }
        }
        else
        {
            // Target out of range
            colliders = null;
        }
        aiData.targets = colliders;
    }

    // It will generate dots on the detected obstacles for debug
    private void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        Gizmos.DrawWireSphere(transform.position, targetDetectionRange);

        if(colliders == null) return;

        Gizmos.color = Color.magenta;
        foreach (var item in colliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }
}
