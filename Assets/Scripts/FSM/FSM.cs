using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

/// <summary>
/// Finite State Machine (FSM) base class for managing states.
/// </summary>
public class FSM : MonoBehaviour
{
    [Tooltip("List of states that the FSM can transition between.")]
    [SerializeField] protected List<State> states = new List<State>();

    protected State currentState;

    private void Start()
    {
        if (states.Count > 0)
            ChangeState(states[0]);
    }

    /// <summary>
    /// Changes the current state of the FSM to a new state.
    /// </summary>
    /// <param name="newState">The new state to change to.</param>
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
