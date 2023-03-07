using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    // AIBehaviour
    [SerializeField]
    List<State> states;

    [field: SerializeField]
    public AIData aiData { get; set; }

    [field: SerializeField]
    public string currentState { get; set; }

    [field: SerializeField]
    public string initialState { get; set; }

    private void Start()
    {
        UpdateState(initialState);
    }

    public void UpdateState(string _newState)
    {
        currentState = _newState;
        foreach (State state in states)
        {
            if (!state.isActive && (state?.triggerState == "" || state?.triggerState == currentState))
                StartCoroutine(state.RunBehaviour(this, aiData));

            if (state?.triggerState != "" && state?.triggerState != currentState)
                StopCoroutine(state.RunBehaviour(this, aiData));
        }
    }
}
