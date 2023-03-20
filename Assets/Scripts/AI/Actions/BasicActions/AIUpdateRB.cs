using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUpdateRB : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    Vector2 velocity;

    public void UpdateVelocity()
    {
        if (!rb || velocity == null) return;
        rb.velocity = velocity;
    }

}
