using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FBKnockback : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    bool StopImpulse = true;

    [SerializeField]
    float strength = 16.0f, delay = 0.15f;

    public UnityEvent OnBegin, OnDone;

    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce( direction * strength, ForceMode2D.Impulse);
        if(StopImpulse) StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
