using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

/// <summary>
/// Script used for collider detection on attack time
/// </summary>
public class WeaponDetection : MonoBehaviour
{
    #region Variables
    public float attackRadius = 0.35f;

    public Transform attackOriginPoint;

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
