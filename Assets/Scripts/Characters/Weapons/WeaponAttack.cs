using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

/// <summary>
/// Script used for attack inputs and control
/// </summary>
public class WeaponAttack : MonoBehaviour
{
    #region variables
    [SerializeField]
    float attackRadius = 0.5f;

    public bool attackBlocked { get; set; }
    #endregion

    #region Methods
    public void PreformAttack(WeaponAnim animator)
    {
        if (!attackBlocked)
        {
            attackBlocked = true;
            animator.PlayAnimation(newAnimations.attack);
        }
    }
    #endregion
}
