using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using Constants;

/// <summary>
/// Script used for collider detection on attack time
/// </summary>
public class WeaponDetection : MonoBehaviour
{
    #region Variables
    [field: Header("Attack")]
    [field: SerializeField]
    public float attackRadius { get; set; }
    [field: SerializeField]
    public Transform attackOriginPoint { get; set; }

    [Header("Player Attr")]
    [SerializeField]
    bool isPlayerWeapon;
    [SerializeField]
    float energyCost = 0.3f;

    bool isAttacking = false;
    #endregion

    #region Methods
    public void DetectColliders()
    {
        if (isAttacking)
        {
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(attackOriginPoint.position, attackRadius))
            {
                if (collider.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy hitted");
                    collider.GetComponent<EnemyHealth>().Hit(gameObject);
                }
            }
        }
    }

    public void EnableDetection()
    {
        isAttacking = true;
        Invoke("DisableDetection", 0.3f);
        if (isPlayerWeapon) ConsumeEnergy();
    }

    public void DisableDetection()
    {
        isAttacking = false;
    }

    void ConsumeEnergy()
    {
        Player.player.health.energy -= energyCost;
    }
    #endregion

    #region Debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = !attackOriginPoint ? Vector3.zero : attackOriginPoint.position;
        Gizmos.DrawWireSphere(position, attackRadius);
    }
    #endregion
}
