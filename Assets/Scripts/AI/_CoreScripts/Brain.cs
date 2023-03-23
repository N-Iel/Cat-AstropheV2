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

    private void OnEnable()
    {
        UpdateState(initialState);
    }

    private void OnDisable()
    {
        foreach (State state in states)
        {
            if(state.corutine != null)
            {
                StopCoroutine(state?.corutine);
                state.corutine = null;
            }
        }
    }

    public void UpdateState(States _newState)
    {
        if (currentState == _newState) return;

        currentState = _newState;
        Debug.Log(_newState);

        foreach (State state in states)
        {
            if (state.corutine == null && state.triggerStates.Exists(x => x == States.None || x == currentState))
            {
                state.corutine = state.RunBehaviour(this, aiData);
                StartCoroutine(state.corutine);
            }

            // This is a redundant call for security proposes
            if (state.corutine != null && state.stopStates.Exists(x => x == States.None || x == currentState))
            {
                StopCoroutine(state?.corutine);
                state.onCorutineStop?.Invoke();
                state.corutine = null;
            }
        }
    }
}
