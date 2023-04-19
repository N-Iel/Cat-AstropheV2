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
    List<int> enemyId = new List<int>();
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
                    if (!enemyId.Contains(collider.GetInstanceID())) 
                    {
                        Player.player.health.RecoverRestrictedSegments(0.2f);
                        Player.player.health.AddEnergy(energyCost / 2);
                        enemyId.Add(collider.GetInstanceID());
                    }
                }

                if (collider.CompareTag("Rock"))
                {
                    collider.GetComponent<Rock>().Hit(gameObject);
                }
            }
        }
    }

    public void EnableDetection()
    {
        enemyId.Clear();
        isAttacking = true;
        Invoke("DisableDetection", 0.3f);
        if (isPlayerWeapon) Player.player.health.AddEnergy(-energyCost);
    }

    public void DisableDetection()
    {
        isAttacking = false;
        Debug.Log("Deshabilitando");
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
