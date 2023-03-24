using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Constants;
using System;
using RengeGames.HealthBars;

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
    float recoverTime = 0.5f,           // Time Between recovery
          hitEnergyCost = 1.0f,         // Amount of energy consumed onHit
          recoverRatio = 0.1f,          // Amount of energy recovered per cycle
          specialRecoverRatio = 0.1f;   // Amount of energy recovered on critical status

    [SerializeField]
    float invincibilityTime = 2.0f;
    public float energy { get; set; }        // Current amount of energy
    public bool isInvincible { get; set; }

    [Header("Components")]
    [SerializeField]
    RadialSegmentedHealthBar energyBar;

    [Header("Events")]
    [SerializeField]
    UnityEvent<GameObject> OnHit;

    [SerializeField]
    UnityEvent<GameObject> OnDeath;  // Events used for feeback effects
    #endregion

    #region lifeCycle
    void Start()
    {
        energy = energyBar.SegmentCount.Value;
        isInvincible = false;
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
            energy = Mathf.Clamp(energy + (!Player.player.isExhausted ? recoverRatio : specialRecoverRatio), 0, energyBar.SegmentCount.Value);

            yield return new WaitForSeconds(recoverTime);
        }
    }

    // Stops Recovery and reduce shield 
    public void Hit(GameObject sender)
    {
        if (Player.player.isDead) return;
        if (Player.player.isExhausted) Dead(sender);

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
        energyBar.SetPercent(energy / energyBar.SegmentCount.Value);

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
    void Dead(GameObject sender)
    {
        Player.player.isDead = true;

        OnDeath?.Invoke(sender);

        Player.player.animator.PlayAnimation(Animations.dead);
        Player.player.rb.velocity = Vector2.zero;
        Player.player.rb.isKinematic = true;

        energy = 0;
    }
    #endregion
}