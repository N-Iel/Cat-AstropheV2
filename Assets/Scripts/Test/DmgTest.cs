using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgTest : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { Player.player.health.Hit(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Player")) { Player.player.health.Hit(gameObject); }
    }
}
