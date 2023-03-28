using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Constants;
using MoreMountains.Feedbacks;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;
    public SpriteRenderer sprite { get; private set; }

    // Eventos
    [SerializeField]
    UnityEvent onDash;

    public delegate void OnFlip();
    public static event OnFlip onFlip;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void PlayAnimation(string _anim)
    {
        switch (_anim)
        {
            case Animations.idle:
                anim?.SetBool("isWalking", false);
                break;

            case Animations.walk:
                anim?.SetBool("isWalking", true);
                break;

            case Animations.dash:
                Debug.Log("dash");
                onDash?.Invoke();
                anim?.SetTrigger("onDash");
                break;

            case Animations.hit:
                anim?.SetTrigger("onHit");
                break;

            case Animations.dead:
                anim?.SetBool("isDead", true);
                break;
        }
    }

    public void UpdateLookingDir(Vector2 _dir)
    {
        // Flip Model
        sprite.flipX = _dir.x < 0;

        // Flip Weapon
        onFlip();
    }
}