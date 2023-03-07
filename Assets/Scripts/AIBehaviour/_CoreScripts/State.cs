using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract string stateName { get; set; }
    public abstract string triggerState { get; set; }
    public abstract string stopState { get; set; }
    public abstract bool isActive { get; set; }

    public abstract IEnumerator RunBehaviour(Brain originBrain, AIData aiData);
}
