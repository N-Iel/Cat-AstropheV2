using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChangeState : MonoBehaviour
{
    [SerializeField]
    States targetState;

    public void nextState(Brain brain)
    {
        if (targetState == States.None) return;
        brain.UpdateState(targetState);
    }
}
