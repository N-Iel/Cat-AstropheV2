using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract string stateName { get; set; }
    public abstract List<States> triggerStates { get; set; }
    public abstract List<States> stopStates { get; set; }
    public abstract bool isActive { get; set; }
    public abstract IEnumerator RunBehaviour(Brain originBrain, AIData aiData);
}
