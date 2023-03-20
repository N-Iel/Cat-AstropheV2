using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

public class Brain : MonoBehaviour
{
    // AIBehaviour
    [SerializeField]
    List<State> states;

    [field: SerializeField]
    public AIData aiData { get; set; }

    [field: SerializeField]
    public States currentState { get; set; }

    [field: SerializeField]
    public States initialState { get; set; }

    private void Start()
    {
        UpdateState(initialState);
    }

    public void UpdateState(States _newState)
    {
        currentState = _newState;
        foreach (State state in states)
        {
            if (!state.isActive && state.triggerStates.Exists(x => x == States.None || x == currentState))
            {
                state.isActive = true;
                StartCoroutine(state.RunBehaviour(this, aiData));
            }

            // This is a redundant call for security proposes
            if (state.stopStates.Exists(x => x == States.None || x == currentState))
            {
                state.isActive = false;
                StopCoroutine(state.RunBehaviour(this, aiData));
            }
        }
    }
}
