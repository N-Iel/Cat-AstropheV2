using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAreaDetector : Detector
{
    [SerializeField]
    float detectionRadius = 1.0f;

    [SerializeField]
    bool showGizmos = false;

    public override void Detect(AIData aiData)
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, detectionRadius))
        {
            if (collider.CompareTag("Player"))
            {
                aiData.targetPosition = collider.transform;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos) return;

        Gizmos.color = Color.red;
        Vector3 position = transform.position;
        Gizmos.DrawWireSphere(position, detectionRadius);
    }
}
