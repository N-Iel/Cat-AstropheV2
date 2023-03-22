using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChangeState : MonoBehaviour
{
    [SerializeField]
    Brain brain;

    [SerializeField]
    States targetState;

    public void nextState()
    {
        if (targetState == States.None || brain.currentState == targetState) return;
            brain.UpdateState(targetState);
    }
}
