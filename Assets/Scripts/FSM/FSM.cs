using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class FSM : MonoBehaviour
{
    [SerializeField] protected List<State> states = new List<State>();

    protected State currentState;

    private void Start()
    {
        if (states.Count > 0)
            ChangeState(states[0]);
    }

    public virtual void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
            currentState.onFinished -= ChangeState;
        }

        currentState = newState;

        currentState.onFinished += ChangeState;

        currentState.Enter(states);
    }

    protected virtual void Update()
    {
        currentState.StateUpdate();
    }
}
