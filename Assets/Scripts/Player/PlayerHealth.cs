using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Constants;
using System;

/// <summary>
/// This script will manage all the elements relative with the energy
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    #region Events
    public delegate void OnExhausted();
    public static event OnExhausted onExhausted;

    public delegate void OnRecover();
    public static event OnRecover onRecover;
    #endregion

    #region variables
    [SerializeField]
    float recoverRatio = 0.1f,  // Amount of energy recovered per cycle
          recoverTime = 0.5f,   // Time Between recovery
          maxEnergy = 3.0f,     // Maximum amount of energy
          hitEnergyCost = 1.0f; // Amount of energy consumed onHit

    [SerializeField]
    float invincibilityTime = 2.0f;
    [SerializeField]
    Image energyBar;


    [Header("Events")]
    [SerializeField]
    UnityEvent<GameObject> OnHit;
    UnityEvent OnDeath;  // Events used for feeback effects

    [NonSerialized]
    public float energy;        // Current amount of energy
    bool isInvincible = false;
    #endregion

    #region lifeCycle
    void Start()
    {
        energy = maxEnergy;
        StartCoroutine(RecoverEnergy());
    }

    void Update()
    {
        if(!Player.player.isDead) UpdateEnergyStatus();
    }
    #endregion

    #region Energy
    // Recovers energy through time
    IEnumerator RecoverEnergy()
    {
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;

        while (!Player.player.isDead)
        {
            energy = Mathf.Clamp(energy + recoverRatio, 0, maxEnergy);

            yield return new WaitForSeconds(recoverTime);
        }
    }

    // Stops Recovery and reduce shield 
    public void Hit(GameObject sender)
    {
        if (isInvincible || Player.player.isDead) return;
        if (Player.player.isExhausted) Dead();

        isInvincible = true;
        energy = Mathf.Floor(energy);
        energy -= hitEnergyCost;

        StopAllCoroutines();
        StartCoroutine(RecoverEnergy());

        OnHit?.Invoke(sender);
        Player.player.animator.PlayAnimation(Animations.hit);
    }

    void UpdateEnergyStatus()
    {
        energyBar.fillAmount = energy / maxEnergy;

        // Player exhausted
        if (energy <= 0 && !Player.player.isExhausted)
        {
            onExhausted();
            Player.player.isExhausted = true;
        }

        // Player recovered
        if (energy >= 1 && Player.player.isExhausted)
        {
            onRecover();
            Player.player.isExhausted = false;
        }
    }
    #endregion

    #region States
    // Updates player status to dead
    void Dead()
    {
        Player.player.isDead = true;

        OnDeath?.Invoke();
        Player.player.animator.PlayAnimation(Animations.dead);

        energy = 0;
    }
    #endregion
}