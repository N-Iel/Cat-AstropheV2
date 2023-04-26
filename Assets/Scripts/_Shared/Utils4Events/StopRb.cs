using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRb : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }
}
