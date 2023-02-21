using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgTest : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { Player.player.health.Hit(); }
    }
}
