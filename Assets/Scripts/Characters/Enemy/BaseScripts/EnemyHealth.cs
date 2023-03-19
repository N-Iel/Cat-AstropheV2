using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script will manage all the elements relative with the enemy's health
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    #region Variables
    // Variables
    [Header("Parameters")]
    [SerializeField]
    int maxHealth = 3;

    [SerializeField]
    float invincibilityTime = 0.15f;

    [SerializeField]
    States triggeredState = States.None;

    [SerializeField]
    [Tooltip("Life required in order to trigger the new state, 0 == every hit")]
    int stateTriggerHealth = 0;

    [SerializeField]
    Brain brain;

    // Non serialized variables
    bool isInvincible;
    int health;
    #endregion

    #region Events
    // Events
    [Header("Events")]
    [SerializeField]
    UnityEvent<GameObject> OnHit;
    [SerializeField]
    UnityEvent OnDead;
    #endregion

    #region LifeCycle
    private void Start()
    {
        // Variables
        health = maxHealth;
    }
    #endregion

    #region Methods

    public void Hit(GameObject gameObject)
    {
        if (isInvincible) return;
        if (health - 1 <= 0)
        {
            Dead();
            return;
        }

        // Apply dmg
        health--;
        isInvincible = true;
        StartCoroutine(Invincibility());

        // Response
        checkStateUpdate();
        OnHit?.Invoke(gameObject);
    }

    void checkStateUpdate()
    {
        if (triggeredState == States.None) return;
        
        if (stateTriggerHealth == 0 || stateTriggerHealth == health ) brain.UpdateState(triggeredState);
    }

    void Dead()
    {
        OnDead?.Invoke();
    }

    IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }
    #endregion

    #region Collider Detectors
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Player.player.health.Hit(gameObject);
    }
    #endregion
}
