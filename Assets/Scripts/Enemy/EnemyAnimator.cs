using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(string _anim)
    {
        switch (_anim)
        { 
            case Animations.walk:
                anim.SetBool("isWalking", true);
                break;

            case Animations.hit:
                anim.SetTrigger("onHit");
                break;

            case Animations.dead:
                anim.SetBool("isDead", true);
                break;

            case Animations.attack:
                anim.SetTrigger("attack");
                break;
        }
    }
}
