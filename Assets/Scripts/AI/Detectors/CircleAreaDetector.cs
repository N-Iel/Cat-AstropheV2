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
        bool targetFound = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                targetFound = true;
                StartCoroutine(SetTargetPosition(aiData, collider.transform.position));
            }
        }
        if (!targetFound) StartCoroutine(SetTargetPosition(aiData, Vector2.zero));
    }

    IEnumerator SetTargetPosition(AIData aiData, Vector2 targetPos)
    {
        yield return new WaitForSeconds(detectionDelay);
        aiData.targetPosition = targetPos;

        // Debug
        detectedPos = aiData.targetPosition;
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
