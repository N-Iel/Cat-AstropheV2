using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

/// <summary>
/// This class will manage all the process related with movement and player control
/// - Cosa a controlar:
///     - Movimiento del personaje
///         - Velocidad
///         - Dirección
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;

    Vector2 direcction = Vector2.zero;

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

        if (direcction != Vector2.zero) Player.player.animator.UpdateLookingDir(direcction);
    }
}