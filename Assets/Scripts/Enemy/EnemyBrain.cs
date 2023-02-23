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
    private List<Detector> detectors;

    [SerializeField]
    private AIData aiData;

    [SerializeField]
    private float detectionDealy = 0.05f; // Stable detection frequency prevents preformance issues
 
    //[SerializeField]
    //UnityEvent Detecting, Attacking, Recovering;

    private void Start()
    {
        InvokeRepeating("PerformDetection", 0, detectionDealy);
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }

    //public void StartDetecting()
    //{
    //    Detecting.Invoke();
    //}

    //public void StartAttacking()
    //{
    //    Attacking.Invoke();
    //}

    //public void StartRecovering() 
    //{ 
    //    Recovering.Invoke();
    //}
}
