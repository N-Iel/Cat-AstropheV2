using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tree : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    float fadespeed = 0;

    Collider2D myTrigger;
    int playerLayer;
    float fade;

    void Start()
    {
        myTrigger = GetComponent<Collider2D>();
        playerLayer = LayerMask.GetMask("Player");
        fadespeed = 0;
    }

    void Update()
    {
        if (!myTrigger) return;

        if (!Physics2D.IsTouchingLayers(myTrigger, playerLayer))
            fade = Mathf.SmoothDamp(spriteRenderer.color.a, 1f, ref fadespeed, 0.2f);
        else
            fade = Mathf.SmoothDamp(spriteRenderer.color.a, 0.5f, ref fadespeed, -0.2f);

        spriteRenderer.color = new Color(1f, 1f, 1f, fade);
    }
}
