using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main script for player functions
/// </summary>
public class Player : MonoBehaviour
{
    #region Variable
    // Variables
    [NonSerialized]
    public bool isDead = false,         // Prevent player from preform all his functions
                isExhausted = false,    // Makes player vulnerable and prevent themF
                isDashing = false;      // Prevents player from moving and attack

    // Components
    public GameObject model;
    public Rigidbody2D rb { get; private set; }
    public CapsuleCollider2D col { get; private set; }

    // Player Scripts
    public static Player player { get; private set; }
    public PlayerMovement movement { get; private set; }
    public PlayerAnimator animator { get; private set; }
    public PlayerHealth health { get; private set; }
    public Shadow shadow { get; private set; }
    #endregion

    private void Awake()
    {
        player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Components
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();

        // Scripts
        shadow = GetComponent<Shadow>();
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        animator = model.GetComponent<PlayerAnimator>();

        // Variables
        isDead = false;
        isExhausted = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Dash interactions
        col.enabled = !isDashing;
        shadow.enabled = isDashing;
    }
}
