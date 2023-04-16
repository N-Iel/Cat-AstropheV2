using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tree : MonoBehaviour
{

    public delegate void TreeHitted();
    public static event TreeHitted treeHitted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Player.player.isDashing)
        {
            treeHitted.Invoke();
            gameObject.SetActive(false);
        }
    }
}
