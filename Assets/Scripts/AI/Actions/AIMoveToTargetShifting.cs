using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveToTargetShifting : MonoBehaviour
{
    [Header("Params")]
    [SerializeField]
    float delay = 0.5f;

    [SerializeField]
    float dirTransitionSpeed = 1.0f;

    Vector2 targetDir;
    Vector2 detectedDir;
    Vector2 desiredDir;
    Vector2 newDir;

    [Header("Components")]
    [SerializeField]
    AIData aIData;

    [SerializeField]
    EnemyActionMove move;

    [Header("Debug")]
    [SerializeField]
    bool gizmos = false;

    private void Start()
    {
        desiredDir = Vector2.zero;
        InvokeRepeating("CalculateDir", 0, delay);
    }

    private void FixedUpdate()
    {
        if (!aIData.currentTarget) return;
        if (Vector2.Distance(desiredDir, newDir) > 0.1f)
            desiredDir = Vector2.Lerp(desiredDir, newDir, (dirTransitionSpeed / Vector2.Distance(desiredDir, newDir)) * Time.deltaTime).normalized;

        move.Move(desiredDir.normalized);
    }

    void CalculateDir()
    {
        if (!aIData.currentTarget) return;
        // Target Pos
        targetDir = (transform.position - aIData.currentTarget.position).normalized;

        // detected Pos
        detectedDir = ((Vector2)transform.position - aIData.detectedPos).normalized;

        // Calculated new Dir
        newDir = (((targetDir * 0.8f) + (detectedDir * 0.2f)) * -1f).normalized;
    }

    private void OnDrawGizmos()
    {
        if (!gizmos || !aIData.currentTarget) return;

        // Target Pos
        Gizmos.color = Color.red;
        Vector2 targetDir = (transform.position - aIData.currentTarget.position).normalized;
        Gizmos.DrawRay((Vector2)transform.position, targetDir * -2f);

        // detected Pos
        Vector2 detectedDir = ((Vector2)transform.position - aIData.detectedPos).normalized;
        Gizmos.DrawRay((Vector2)transform.position, detectedDir * -1f);

        // new dir
        Gizmos.color = Color.yellow;
        Vector2 dir = (targetDir * 0.8f) + (detectedDir * 0.2f);
        Gizmos.DrawRay(transform.position, dir * -1f);

        // desired dir
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, desiredDir);
    }
}
