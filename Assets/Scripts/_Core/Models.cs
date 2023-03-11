using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public interface Movement
    {
        // Serializable
        float speed { get; set; }
        float acceleration { get; set; }
        float maxSpeed { get; set; }
        float decceleration { get; set; }

        // Non Serializable
        Rigidbody2D rb { get; set; }
        Vector2 direction { get; set; }
    }
}
