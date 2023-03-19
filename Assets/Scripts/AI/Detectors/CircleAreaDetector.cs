using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAreaDetector : Detector
{
    [SerializeField]
    float detectionRadius = 1.0f;

    [SerializeField]
    [Tooltip("Delay between target is detected and position is stored, improves targeting feeling")]
    float detectionDelay = 0.5f;

    // Gizmos
    [SerializeField]
    bool showGizmos = false;
    Vector2 detectedPos;

    public override void Detect(AIData aiData)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
                StartCoroutine(SetTargetPosition(aiData, collider.transform));
        }
        //if (!targetFound) StartCoroutine(SetTargetPosition(aiData, null));
    }

    IEnumerator SetTargetPosition(AIData aiData, Transform target)
    {
        yield return new WaitForSeconds(detectionDelay);
        //aiData.targetPosition = targetPos;
        aiData.currentTarget = target;
        Debug.Log("TargetUpdated");

        // Debug
        detectedPos = (Vector2)target?.position;
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos) return;

        // Area of effect
        Gizmos.color = Color.red;
        Vector3 position = transform.position;
        Gizmos.DrawWireSphere(position, detectionRadius);

        if (detectedPos == Vector2.zero) return;

        // Detected Pos
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(detectedPos, 0.3f);
    }
}
