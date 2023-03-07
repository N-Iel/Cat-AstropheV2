using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will check for trigger collisions with the player
/// </summary>
public class EnemyCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) Player.player.health.Hit(gameObject);
    }
}
