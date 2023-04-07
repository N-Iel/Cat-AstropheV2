using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FBKnockback : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float strength = 16.0f, delay = 0.15f;

    [SerializeField]
    [Tooltip("Add the own velocity to the force in order to cancel the inertia")]
    bool addRbVelocity = false;

    public UnityEvent OnBegin, OnDone;

    public void PlayFeedback(GameObject sender)
    {
        float totalForce = !addRbVelocity ? strength : strength + rb.velocity.magnitude;

        StopAllCoroutines();

        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        //rb.AddForce(direction * totalForce, ForceMode2D.Impulse);
        rb.velocity += direction * totalForce;

        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        OnDone?.Invoke();
    }
}
