using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.TextCore.Text;

/// <summary>
/// Tengo que hacer un movimiento que alcance una velocidad pico y reduzca la velocidad en función de la distancia al objetivo
/// </summary>
public static class MainMovement
{
    public static void ApplyMovement(Movement _movement)
    {
        if (_movement.force == 0) GetForce(_movement);
        //float speed = Vector3.SmoothDamp(_movement.);
    }

    private static void GetForce(Movement _movement)
    {
        // F = ((Vf - Vi)*m)/(t * x)
        _movement.force =
            ((_movement.maxSpeed - _movement.rb.velocity.magnitude) * _movement.rb.mass)
            / _movement.timeToReachTarget;
    }
}
