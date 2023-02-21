using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FeedbackTester : MonoBehaviour
{
    [SerializeField]
    UnityEvent<GameObject> OnHit;

    public void Hit(GameObject sender)
    {
        OnHit.Invoke(sender);
    }
}
