using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using UnityEngine.Events;

/// <summary>
/// This will handle dash and shadow generation
/// </summary>
public class PlayerDash : MonoBehaviour
{
    // Dash Params
    public float dashingPower = 24f,
                 dashingTime = 0.2f,
                 dashingCooldown = 1f,
                 dashingCost = 0.8f;

    [SerializeField]
    UnityEvent OnDash;

    bool dashAvailable = true;

    // Update is called once per frame
    void Update()
    {
        DashInput();
    }

    void DashInput()
    {
        if (dashAvailable && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift)) && !Player.player.isExhausted) StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        // Update Player state
        Player.player.animator.PlayAnimation(Animations.dash);
        Player.player.health.energy -= dashingCost;
        Player.player.isDashing = true;
        dashAvailable = false;

        Player.player.rb.velocity = Player.player.movement.lastDir * dashingPower;

        yield return new WaitForSeconds(dashingTime);

        Player.player.isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);

        dashAvailable = true;
    }
}
