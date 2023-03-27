using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Constants;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim { get; private set; }
    public AudioSource source { get; private set; }

    private void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    public void PlayAnimation(string _anim)
    {
        switch (_anim)
        {
            case Animations.idle:
                anim.SetBool("isWalking", false);
                break;

            case Animations.walk:
                anim.SetBool("isWalking", true);
                break;

            case Animations.dash:
                anim.SetTrigger("onDash");
                break;

            case Animations.hit:
                anim.SetTrigger("onHit");
                break;

            case Animations.dead:
                anim.SetBool("isDead", true);
                break;
        }
    }

    public void UpdateLookingDir(Vector2 _dir)
    {
        // Flip Model
        Vector2 lastDir = Player.player.model.transform.localScale;
        lastDir.x = Mathf.Sign(_dir.x);
        Player.player.model.transform.localScale = lastDir;
    }
}