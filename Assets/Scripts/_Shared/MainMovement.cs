using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using UnityEngine.TextCore.Text;

public static class MainMovement
{
    public static void ApplyMovement(Movement _movement)
    {
        if (_movement.direction.magnitude > 0 && _movement.speed >= 0)
        {
            _movement.speed += _movement.acceleration * _movement.maxSpeed * Time.deltaTime;
            //animator.UpdateLookingDir();
        }
        else
        {
            _movement.speed -= _movement.decceleration * _movement.maxSpeed * Time.deltaTime;
        }
        _movement.speed = Mathf.Clamp(_movement.speed, 0, _movement.maxSpeed);
        _movement.rb.velocity = _movement.direction * _movement.speed;
    }
}
