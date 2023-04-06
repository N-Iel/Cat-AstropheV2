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
    Vector2 detectedPos = Vector2.zero;
    Vector2 initialPos = Vector2.zero;

    public override void Detect(AIData aiData)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(targetTag))
            {
                aiData.currentTarget = collider.transform;
                detectedPos = aiData.currentTarget.position;
                if (aiData.detectedPos == Vector2.zero)
                {
                    aiData.detectedPos = aiData.currentTarget.position;
                    initialPos = aiData.detectedPos;
                }
                break;
            }
        }
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

        // initial Pos
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(initialPos, 0.3f);
    }
}
