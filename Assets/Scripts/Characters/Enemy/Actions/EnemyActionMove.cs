using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionMove : MonoBehaviour
{
    #region Variables
    [Header("Parámeters")]
    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float acceleration;

    [SerializeField]
    float decceleration;
    public bool canMove { get; set; }

    [Header("Components")]
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    EnemyAnimator animator;

    // Private variables
    float speed;
    #endregion

    private void Start()
    {
        canMove = true;
    }

    public void Move(Vector2 direction)
    {
        if (!canMove) return;

        if (direction.magnitude > 0 && speed >= 0)
        {
            speed += acceleration * maxSpeed * Time.deltaTime;
            animator.UpdateLookingDir(direction);
        }
        else
        {
            speed -= decceleration * maxSpeed * Time.deltaTime;
        }
        speed = Mathf.Clamp(speed, 0, maxSpeed);
        rb.velocity = direction * speed;
    }
}
