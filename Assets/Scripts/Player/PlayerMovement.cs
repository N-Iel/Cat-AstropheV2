using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

/// <summary>
/// Preforms all the task related with player movent throught inputs
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;

    Vector2 direcction = Vector2.zero;
    public Vector2 lastDir { get; private set; } // Last direction used that is not zero


    void Update()
    {
        Inputs();
        Animate();
    }

    void FixedUpdate()
    {
        if (!Player.player.isDashing && !Player.player.isDead)
            Move();
    }

    void Inputs()
    {
        direcction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void Animate()
    {
        if (direcction != Vector2.zero)
            Player.player.animator.PlayAnimation(Animations.walk);
        else
            Player.player.animator.PlayAnimation(Animations.idle);
    }

    void Move()
    {
        Player.player.rb.MovePosition(Player.player.rb.position + direcction * speed * Time.deltaTime);

        if (direcction != Vector2.zero)
        {
            lastDir = direcction;
            Player.player.animator.UpdateLookingDir(direcction);
        }
    }
}