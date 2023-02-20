using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

/// <summary>
/// Script used for attack inputs and control
/// </summary>
public class WeaponAttack : MonoBehaviour
{
    [SerializeField]
    float attackCooldown = 0.2f,
          attackRadius = 0.5f;

    bool attackBlocked;

    public void PreformAttack(WeaponAnim animator)
    {
        if (!attackBlocked)
        {
            attackBlocked = true;
            animator.PlayAnimation(Animations.attack);
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackCooldown);
        attackBlocked = false;
    }
}
