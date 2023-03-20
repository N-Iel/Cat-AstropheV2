using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

public class CircleAreaDetector : Detector
{
    [SerializeField]
    float detectionRadius = 1.0f;

    [SerializeField]
    [Tooltip("Delay between target is detected and position is stored, improves targeting feeling")]
    float detectionDelay = 0.5f;

    [SerializeField]
    string targetTag = "Player"; 

    // Gizmos
    [SerializeField]
    bool showGizmos = false;
    Vector2 detectedPos;

    public override void Detect(AIData aiData)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(targetTag))
                StartCoroutine(SetTargetPosition(aiData, collider.transform));
        }
    }

    IEnumerator SetTargetPosition(AIData aiData, Transform target)
    {
        yield return new WaitForSeconds(detectionDelay);
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
