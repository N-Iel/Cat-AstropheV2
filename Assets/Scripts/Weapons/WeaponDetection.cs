using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script used for collider detection on attack time
/// </summary>
public class WeaponDetection : MonoBehaviour
{
    public float attackRadius = 0.35f;

    public Transform attackOriginPoint;
    bool isAttacking = false;

    public void DetectColliders()
    {
        if (isAttacking)
        {
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(attackOriginPoint.position, attackRadius))
            {

                Debug.Log(collider.name);
            }
        }
    }

    public void EnableDetection()
    {
        isAttacking = true;
    }

    public void DisableDetection()
    {
        isAttacking = false;
    }

    // Debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = !attackOriginPoint ? Vector3.zero : attackOriginPoint.position;
        Gizmos.DrawWireSphere(position, attackRadius);
    }
}
