using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Constants;
using System;
using RengeGames.HealthBars;
using static UnityEditor.Experimental.GraphView.GraphView;

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
          hitEnergyCost = 1.0f;         // Amount of energy consumed onHit

    [field: SerializeField]
    public float recoverRatio { get; set; }

    [SerializeField]
    float invincibilityTime = 2.0f;
    public float energy { get; set; }        // Current amount of energy
    public bool isInvincible { get; set; }

    float restrictedSegments = 0f;
    float recoverSegment = 0f;

    [Header("Components")]
    [SerializeField]
    RadialSegmentedHealthBar energyBar;
    [SerializeField]
    RadialSegmentedHealthBar brokenBar;

    [Header("Events")]
    [SerializeField]
    UnityEvent<GameObject> OnHit;
    [SerializeField]
    UnityEvent<GameObject> OnDeath;  // Events used for feeback effects
    [SerializeField]
    UnityEvent OnSegmentRecovered;  // Events used for feeback effects
    [SerializeField]
    UnityEvent EnergyWarning;  // Events used for feeback effects
    #endregion

    #region lifeCycle
    void Start()
    {
        energy = energyBar.SegmentCount.Value;
        isInvincible = false;
        StartCoroutine(RecoverEnergy());
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
            AddEnergy(!Player.player.isExhausted ? recoverRatio : recoverRatio * 2);

            yield return new WaitForSeconds(recoverTime);
        }
    }

    // Stops Recovery and reduce shield 
    public void Hit(GameObject sender)
    {
        if (Player.player.isDead) return;
        if (Player.player.isExhausted) Dead(sender);

        isInvincible = true;
        AddRestrictedSegments(1);

        StopAllCoroutines();
        StartCoroutine(RecoverEnergy());

        OnHit?.Invoke(sender);
        Player.player.animator.PlayAnimation(Animations.hit);     
    }

    public void AddEnergy(float _energy)
    {
        energy = Mathf.Clamp(energy + _energy, 0, energyBar.SegmentCount.Value - restrictedSegments);
        energyBar.SetRemovedSegments(energyBar.SegmentCount.Value - energy);

        // Player exhausted
        if (energy <= 0 && Mathf.Abs(_energy) >= 1 && !Player.player.isExhausted)
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

    public void AddRestrictedSegments(float _restriction)
    {
        int energyConsume = restrictedSegments >= energyBar.SegmentCount.Value - 1 && _restriction > 0 ? -1 : 0;
        restrictedSegments = Mathf.Clamp(restrictedSegments + _restriction, 0, energyBar.SegmentCount.Value - 1);
        brokenBar.SetRemovedSegments(brokenBar.SegmentCount.Value - (restrictedSegments - recoverSegment));
        AddEnergy(energyConsume);
    }

    public void RecoverRestrictedSegments(float _recovered)
    {
        if (restrictedSegments <= 0) return;
        if (recoverSegment + _recovered >= 1)
        {
            recoverSegment = 0;
            AddRestrictedSegments(-1);
            OnSegmentRecovered?.Invoke();
            return;
        }
        recoverSegment += _recovered;
        brokenBar.SetRemovedSegments((brokenBar.SegmentCount.Value - restrictedSegments) + recoverSegment);
        // OnSegmentRecoverFeedback;
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