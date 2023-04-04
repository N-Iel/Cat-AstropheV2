using System.Collections;
using UnityEngine;

public class CircleAreaDetector : Detector
{
    [SerializeField]
    float detectionRadius = 1.0f;

    [SerializeField]
    string targetTag = "Player"; 

    // Gizmos
    [SerializeField]
    bool showGizmos = false;
    Vector2 detectedPos;

    private void OnDisable()
    {
        detectedPos = Vector2.zero;
    }

    public override void Detect(AIData aiData)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(targetTag))
            {
                aiData.currentTarget = collider.transform;
                detectedPos = (Vector2)aiData.currentTarget.position;
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos || detectedPos == Vector2.zero) return;

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
