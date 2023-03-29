using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Constants;
using UnityEngine.Events;

/// <summary>
/// Preforms all the task related with dashing
/// </summary>
public class PlayerDash : MonoBehaviour
{
    #region Variables
    // Dash Params
    public float dashingPower = 24f,
                 dashingTime = 0.2f,
                 dashingCooldown = 1f,
                 dashingCost = 0.8f;

    public InputActionReference input;

    [SerializeField]
    UnityEvent OnDash;  // Event used for feeback effects

    bool dashAvailable = true;
    #endregion

    #region Events
    private void OnEnable()
    {
        input.action.started += DashInput;
    }

    private void OnDisable()
    {
        input.action.started -= DashInput;
    }
    #endregion

    #region Methods
    void DashInput(InputAction.CallbackContext obj)
    {
        if (Time.timeScale == 0) return;
        if (dashAvailable && !Player.player.isExhausted) StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        // Update Player state
        Player.player.animator.PlayAnimation(Animations.dash);
        Player.player.health.AddEnergy(-dashingCost);
        Player.player.isDashing = true;
        dashAvailable = false;

        Player.player.rb.velocity = Player.player.movement.lastDir * dashingPower;

        yield return new WaitForSeconds(dashingTime);

        Player.player.isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);

        dashAvailable = true;
    }

    void OnEnemyAvoided()
    {
        Player.player.health.RecoverRestrictedSegments(0.2f);
    }
    #endregion
}
