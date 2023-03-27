using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Constants;
using MoreMountains.Feedbacks;
using System.Threading.Tasks;

/// <summary>
/// Script used for weapon Animations
/// </summary>
public class WeaponAnim : MonoBehaviour
{
    // Components
    public Animator anim { get; set; }
    WeaponAttack attack;

    // Feedback
    [SerializeField]
    MMF_Player feedback;

    MMF_Rotation rotationFB;

    // Events
    [SerializeField]
    UnityEvent onAttack, onExhausted, onRecover;

    private void OnEnable()
    {
        PlayerHealth.onExhausted += FadeOutWeapon;
        PlayerHealth.onRecover += FadeInWeapon;
        PlayerAnimator.onFlip += Flip;
        attack = GetComponent<WeaponAttack>();
        rotationFB = feedback.GetFeedbackOfType<MMF_Rotation>();
    }
    private void OnDisable()
    {
        PlayerHealth.onExhausted -= FadeOutWeapon;
        PlayerHealth.onRecover -= FadeInWeapon;
        PlayerAnimator.onFlip -= Flip;
    }

    public void PlayAnimation(string _anim)
    {
        switch (_anim)
        {
            case Animations.attack:
                onAttack?.Invoke();
                break;
        }
    }
    void FadeOutWeapon()
    {
        StartCoroutine(waitForAttackToFinish());
    }
    void FadeInWeapon()
    {
        onRecover?.Invoke();
    }
    IEnumerator waitForAttackToFinish()
    {
        yield return new WaitUntil(() => !attack.attackBlocked);
        onExhausted?.Invoke();
    }
    private void Flip()
    {
        if (Player.player.isExhausted) return;
        rotationFB.RemapCurveOne = -90 * transform.localScale.x;
    }
}
