using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tree : MonoBehaviour
{
    public delegate void TreeHitted();
    public static event TreeHitted treeHitted;

    bool hitted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hitted && collision.CompareTag("Player") && Player.player.isDashing)
        {
            hitted = true;
            treeHitted?.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!hitted && collision.CompareTag("Player") && Player.player.isDashing)
        {
            hitted = true;
            treeHitted?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
