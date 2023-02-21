using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Constants;

/// <summary>
/// Script used for weapon Animations
/// </summary>
public class WeaponAnim : MonoBehaviour
{
    public Animator anim;

    private void OnEnable()
    {
        PlayerHealth.onExhausted += FadeOutWeapon;
        PlayerHealth.onRecover += FadeInWeapon;
    }
    private void OnDisable()
    {
        PlayerHealth.onExhausted -= FadeOutWeapon;
        PlayerHealth.onRecover -= FadeInWeapon;
    }

    public void PlayAnimation(string _anim)
    {
        switch (_anim)
        {
            case Animations.idle:
                anim.SetBool("isWalking", false);
                break;

            case Animations.attack:
                anim.SetTrigger("onAttack");
                break;
        }
    }
    void FadeOutWeapon()
    {
        anim.SetTrigger("onExhausted");
    }
    void FadeInWeapon()
    {
        anim.SetTrigger("onRecover");
    }
}
