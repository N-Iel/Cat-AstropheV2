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
}
