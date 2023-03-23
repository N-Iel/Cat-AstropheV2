using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Al morir, el búho recibirá el knockback del golpe, dejará de acelerar y al x tiempo desaparecerá y será posible que vuelva a aparecer
/// Resumen:
///     - Desactivar movimiento
///     - Desactivar collider
///     - Activar rotación en el rb
///     - Invocar un método a los x milisegunos para triggerear la animación y al acabar esta:
///         - Mover el modelo al 0,0
///         - Desactivar el gameobject completo
/// </summary>
public class onOwlDead : MonoBehaviour
{
    [SerializeField]
    float postMortenDelay = 1.0f;

    [Header("Components")]
    [SerializeField]
    GameObject model;

    [SerializeField]
    EnemyAnimator animator;
    public void onDead()
    {
        animator.PlayAnimation(newAnimations.dead);
        Invoke("PostMorten", postMortenDelay);
    }

    private void PostMorten()
    {
        Debug.Log("postmorten");
        this.enabled = false;
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        model.transform.parent.gameObject.SetActive(false);
    }
}
