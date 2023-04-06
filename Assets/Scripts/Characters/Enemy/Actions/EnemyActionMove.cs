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

    [SerializeField]
    [Range(1,10)]
    float speedVariationLimit;

    [SerializeField]
    [Tooltip("Amount of time used before randomize speed variation again")]
    [Range(0.5f, 5f)]
    float variationRateLimit;
    public bool canMove { get; set; }

    [Header("Components")]
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    EnemyAnimator animator;

    // Private variables
    public float speed { get; set; }
    float speedVariation;
    #endregion

    private void Start()
    {
        canMove = true;
        StartCoroutine(UpdateSpeedVariation());
    }

    public void Move(Vector2 direction)
    {
        if (!canMove) return;

        if (direction.magnitude > 0 && speed >= 0)
        {
            speed += acceleration * (maxSpeed + speedVariation) * Time.deltaTime;
            animator.UpdateLookingDir(direction);
        }
        else
        {
            speed -= decceleration * (maxSpeed + speedVariation) * Time.deltaTime;
        }
        speed = Mathf.Clamp(speed, 0, (maxSpeed + speedVariation));
        rb.velocity = direction * speed;
    }

    IEnumerator UpdateSpeedVariation()
    {
        float _speedVariation = Random.Range(-speedVariationLimit, speedVariationLimit);
        do
        {
            speedVariation = Mathf.Lerp(speedVariation, _speedVariation, 0.5f);
        } while (Mathf.Approximately(speedVariation, _speedVariation));
        Debug.Log($"New speed: {speedVariation}");
        float variationRate = Random.Range(0.5f,variationRateLimit);
        yield return new WaitForSeconds(variationRate);
        StartCoroutine(UpdateSpeedVariation());
    }
}
