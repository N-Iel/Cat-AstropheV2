using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script will manage all the elements relative with the enemy's health
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    // Variables
    [SerializeField]
    int maxHealth = 3;
    [SerializeField]
    float invincibilityTime = 0.15f;

    bool isInvincible;
    int health;

    // Eventos
    [SerializeField]
    UnityEvent<GameObject> OnHit;
    [SerializeField]
    UnityEvent OnDead;

    private void Start()
    {
        health = maxHealth;
    }

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
}
