using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Rock : MonoBehaviour
{
    [SerializeField]
    int hitsRequired = 3;
    int hits = 0;
    bool hitted = false;

    [SerializeField]
    UnityEvent onHit;

    public delegate void RockDestroyed();
    public static event RockDestroyed rockDestroyed;

    private void OnEnable()
    {
        hits = 0;
    }

    // TODO Use source for particles and effects
    public async void Hit(GameObject source)
    {
        if(hitted) return;

        Debug.Log("Rock Hitted");
        hitted = true;
        onHit.Invoke();
        hits++;

        if (hits >= hitsRequired)
        {
            rockDestroyed.Invoke();
            gameObject.SetActive(false);
        }

        await Task.Delay(1000);

        hitted = false;
    }
}
