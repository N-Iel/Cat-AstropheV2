using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FeedbackTester : MonoBehaviour
{
    [SerializeField]
    UnityEvent<GameObject> OnHit;

    float invincibilityTime = 0.2f;
    float counter = 0.2f;

    private void Update()
    {
        counter += Time.deltaTime;
    }

    public void Hit(GameObject sender)
    {
        if(counter >= invincibilityTime)
        {
            Debug.Log("hitted");
            counter = 0;
            OnHit.Invoke(sender);
        }
    }
}
