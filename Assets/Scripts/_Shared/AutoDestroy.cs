using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField]
    [Tooltip("If the time is '0' it wont get destroyed")]
    float time = 0f;

    private void Start()
    {
        if (time > 0) Invoke("DestroySelf", time);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
