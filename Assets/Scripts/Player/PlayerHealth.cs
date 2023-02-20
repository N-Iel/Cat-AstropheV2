using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Constants;

/// <summary>
/// This script will manage all the elements relative with the energy
/// TODO: Energy display
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    float recoverDelay = 1.0f,  // Time needed before start recovering
          recoverRatio = 0.2f,  // Amount of energy recovered per cycle
          recoverRate = 0.2f,   // Time Between recovery
          maxEnergy = 3.0f;     // Maximum amount of energy

    [SerializeField]
    float hitEnergyCost = 1.0f; // Amount of energy consumed onHit

    [Header("Events")]
    UnityEvent OnHit, OnDeath;  // Events used for feeback effects

    public float energy;        // Current amount of energy

    // Recovers energy through time
    IEnumerator RecoverEnergy()
    {
        yield return new WaitForSeconds(recoverDelay);

        while (!Player.player.isDead)
        {
            energy = Mathf.Clamp(energy + recoverRatio, 0, maxEnergy);
            if (energy >= 1) Player.player.isExhausted = false;
            yield return new WaitForSeconds(recoverRate);
        }
    }

    // Stops Recovery and reduce shield 
    public void Hit()
    {
        if (Player.player.isExhausted) Dead();

        StopAllCoroutines();
        StartCoroutine(RecoverEnergy());

        OnHit.Invoke();
        Player.player.animator.PlayAnimation(Animations.hit);

        energy -= hitEnergyCost;
    }

    // Updates player status to dead
    void Dead()
    {
        Player.player.isDead = true;

        OnDeath.Invoke();
        Player.player.animator.PlayAnimation(Animations.dead);

        energy = 0;
    }
}