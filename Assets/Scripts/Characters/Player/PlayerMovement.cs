using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Constants;

/// <summary>
/// Preforms all the task related with player movent throught inputs
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    #region variables
    [SerializeField]
    float maxSpeed = 15f, autumnSpeed = 18f, acceleration = 50f, decceleration = 100f;
    float speed;

    [SerializeField]
    InputActionReference input;
    public Vector2 lastDir { get; private set; } // Last  used that is not zero

    Vector2 direction = Vector2.zero;
    #endregion

    #region LifeCycle
    void Update()
    {
        if (Time.timeScale == 0) return;
        Inputs();
        Animate();
    }

    void FixedUpdate()
    {
        if (!Player.player.isDashing && !Player.player.isDead)
            Move();
    }
    #endregion

    #region Methods
    void Inputs()
    {
       direction = input.action.ReadValue<Vector2>();
    }

    void Animate()
    {
        if (direction != Vector2.zero)
            Player.player.animator.PlayAnimation(Animations.walk);
        else
            Player.player.animator.PlayAnimation(Animations.idle);
    }

    void Move()
    {
        //Player.player.rb.MovePosition(Player.player.rb.position +  * speed * Time.deltaTime);
        if (direction.magnitude > 0 && speed >= 0)
        {
            speed += acceleration * maxSpeed * Time.deltaTime;
            lastDir = direction;
            Player.player.animator.UpdateLookingDir(direction);

            if (SeasonManager.seasonManager?.currentSeason != null)
            {
                if (SeasonManager.seasonManager?.currentSeason.season == Seasons.Winter && Player.player.health.recoverRatio < 0)
                    Player.player.health.recoverRatio *= -1;

                if (SeasonManager.seasonManager?.currentSeason.season == Seasons.Summer && Player.player.health.recoverRatio > 0)
                    Player.player.health.recoverRatio *= -1;

            }
        }
        else
        {
            speed -= decceleration * maxSpeed * Time.deltaTime;

            if (SeasonManager.seasonManager?.currentSeason != null)
            {
                if (SeasonManager.seasonManager?.currentSeason.season == Seasons.Summer && Player.player.health.recoverRatio < 0)
                    Player.player.health.recoverRatio *= -1;

                if (SeasonManager.seasonManager?.currentSeason.season == Seasons.Winter && Player.player.health.recoverRatio > 0)
                    Player.player.health.recoverRatio *= -1;
            }
        }
        speed = Mathf.Clamp(speed, 0, SeasonManager.seasonManager?.currentSeason?.season == Seasons.Autumn ? autumnSpeed : maxSpeed);
        Player.player.rb.velocity = direction * speed;
    }
    #endregion
}