using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Finite State Machine (FSM) controlling the behavior of a stove counter in the kitchen.
/// </summary>
public class StoveCounterFSM : FSM
{
    [Tooltip("Reference to the stove counter associated with this FSM.")]
    [SerializeField] private StoveCounter stoveCounter;

    /// <summary>
    /// Changes the current state of the FSM.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    public override void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
            currentState.onFinished -= ChangeState;
        }

        currentState = newState;

        currentState.onFinished += ChangeState;

        currentState.Enter(states, stoveCounter);
    }

    /// <summary>
    /// Overrides the Update method to limit state updates to when the stove counter has a kitchen object.
    /// </summary>
    protected override void Update()
    {
        if(stoveCounter.HasKitchenObject())
        {
            base.Update();
        }
    }

    /// <summary>
    /// Changes the state to a specific state of type T.
    /// </summary>
    /// <typeparam name="T">The type of state to transition to.</typeparam>
    public void OnStoveInteract<T>() where T : State
    {
        foreach (State state in states)
        {
            if(state as  T)
            {
                ChangeState(state);
            }
        }
    }
}
