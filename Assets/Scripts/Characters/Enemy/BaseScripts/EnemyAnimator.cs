using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Constants;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 1f;

    [SerializeField]
    GameObject enemyModel;

    [SerializeField]
    SpriteRenderer renderer;

    [SerializeField]
    Animator anim;

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
    public void UpdateLookingDir(Vector2 _dir)
    {
        // Flip Model
        Vector2 lastDir = enemyModel.transform.localScale;
        lastDir.x = Mathf.Sign(_dir.x);
        enemyModel.transform.localScale = lastDir;
    }

    public void RotatoToLookingDir(Vector2 _dir)
    {
        // Rotate model
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, _dir) * Quaternion.Euler(0, 0, 90);
        enemyModel.transform.localRotation = rotation;
        renderer.flipY = Mathf.Abs(enemyModel.transform.localEulerAngles.z) > 90;
    }
}
