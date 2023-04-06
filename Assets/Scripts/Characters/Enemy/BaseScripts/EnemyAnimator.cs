using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Constants;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sprRenderer;
    [SerializeField]
    Animator anim;

    private void Awake()
    {
        if(!sprRenderer) sprRenderer = GetComponent<SpriteRenderer>();
        if(!anim) anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        sprRenderer.enabled = true;
    }

    public void PlayAnimation(newAnimations _anim)
    {
        switch (_anim)
        { 
            case newAnimations.idle:
                anim.SetBool("isWalking", true);
                break;

            case newAnimations.hit:
                anim.SetTrigger("onHit");
                break;

            case newAnimations.dead:
                anim.SetBool("isDead", true);
                break;

            case newAnimations.attack:
                anim.SetTrigger("attack");
                break;

            case newAnimations.reset:
                anim.SetTrigger("originReached");
                break;
        }
    }

    public void UpdateLookingDir(Vector2 _dir)
    {
        // Flip Model
        Vector2 lastDir = gameObject.transform.localScale;
        lastDir.x = Mathf.Sign(_dir.x);
        gameObject.transform.localScale = lastDir;
    }

    public void RotatoToLookingDir(Vector2 _dir)
    {
        // Rotate model
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, _dir) * Quaternion.Euler(0, 0, 90);
        gameObject.transform.localRotation = rotation;
        sprRenderer.flipY = Mathf.Abs(gameObject.transform.localEulerAngles.z) > 85;
    }
}
