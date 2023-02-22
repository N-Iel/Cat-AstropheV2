using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script will manage all the states and acctions related with IA
/// </summary>
public class EnemyBrain : MonoBehaviour
{
    [SerializeField]
    UnityEvent Detecting, Attacking, Recovering;

    private void Start()
    {
        StartDetecting();
    }

    public void StartDetecting()
    {
        Detecting.Invoke();
    }

    public void StartAttacking()
    {
        Attacking.Invoke();
    }

    public void StartRecovering() 
    { 
        Recovering.Invoke();
    }
}
