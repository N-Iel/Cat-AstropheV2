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
    Enemies id;

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
    [Tooltip("Restore health on enable")]
    bool restoreOnEnable = true;

    [SerializeField]
    bool resetPositionOnEnable = false;

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
    UnityEvent<GameObject> OnDead;

    public delegate void OnKill(Enemies id);
    public static event OnKill onKill;
    #endregion

    #region LifeCycle
    private void Start()
    {
        RestoreHealth();
    }
    private void OnEnable()
    {
        if (restoreOnEnable) RestoreHealth();
        isInvincible = false;
        if (resetPositionOnEnable) gameObject.transform.localPosition = Vector3.zero;
    }
    #endregion

    #region Methods

    public void Hit(GameObject gameObject)
    {
        if (isInvincible) return;

        if (!isInvincible && health - 1 <= 0)
        {
            Dead(gameObject);
            return;
        }

        isInvincible = true;

        // Apply dmg
        health--;
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

    void Dead(GameObject gameObject)
    {
        OnDead?.Invoke(gameObject);
        onKill?.Invoke(id);
    }

    public void RestoreHealth()
    {
        health = maxHealth;
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
