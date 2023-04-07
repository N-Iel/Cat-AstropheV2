using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBShadowTrigger : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    AIData aiData;

    [SerializeField]
    Shadow shadow;

    private void Update()
    {
        if (!aiData.currentTarget) return;

        Vector2 targetDir = (transform.position - aiData.currentTarget.position).normalized;
        shadow.enabled = Vector2.Distance(targetDir * -1, rb.velocity.normalized) < 0.1f;
    }
}
