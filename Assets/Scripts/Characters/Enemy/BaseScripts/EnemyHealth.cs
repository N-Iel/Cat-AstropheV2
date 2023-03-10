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

    // Non serialized variables
    bool isInvincible { get; set; }
    int health { get; set; }
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
        health = maxHealth;
    }
    #endregion

    #region Methods

    // Metodos
    public void Hit(GameObject gameObject)
    {
        if (isInvincible) return;
        if (health - 1 <= 0) Dead();

        health--;
        isInvincible = true;
        StartCoroutine(Invincibility());
        OnHit?.Invoke(gameObject);
    }

    void Dead()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
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
        if(collision.gameObject.CompareTag("Player"))
            Hit(collision.gameObject);
    }
    #endregion
}
