using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public interface Movement
    {
        // Serializable
        float maxSpeed { get; set; } // Vf
        float timeToReachTarget { get; set; } // t

        // Non Serializable
        float force { get; set; }
        float distance { get; set; }
        Rigidbody2D rb { get; set; }
        Vector2 direction { get; set; }
    }
}
