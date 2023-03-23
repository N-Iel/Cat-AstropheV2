using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Al morir, el b�ho recibir� el knockback del golpe, dejar� de acelerar y al x tiempo desaparecer� y ser� posible que vuelva a aparecer
/// Resumen:
///     - Desactivar movimiento
///     - Desactivar collider
///     - Activar rotaci�n en el rb
///     - Invocar un m�todo a los x milisegunos para triggerear la animaci�n y al acabar esta:
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
