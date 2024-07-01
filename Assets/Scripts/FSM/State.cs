using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for defining states in a finite state machine (FSM).
/// </summary>
public abstract class State : ScriptableObject
{
    [Flags]
    public enum ShouldLog
    {
        None = 0,
        Update,
        FixedUpdate,
        LateUpdate,
    }
    protected List<State> nextStates;

    /// <summary>
    /// Event triggered when the state finishes.
    /// </summary>
    public event Action<State> onFinished;

    [Tooltip("Enable logging for Update events.")]
    [SerializeField] private bool shouldLogUpdate = false;

    /// <summary>
    /// Method called when entering this state.
    /// </summary>
    /// <param name="possibleStates">List of possible next states.</param>
    /// <param name="objects">Optional parameters for state initialization.</param>
    public virtual void Enter(List<State> possibleStates, params object[] objects)
    {
        nextStates = possibleStates;
    }

    /// <summary>
    /// Update method called once per frame for this state.
    /// </summary>
    public virtual void StateUpdate()
    {
        if(shouldLogUpdate)
            Debug.Log("Update");
    }

    /// <summary>
    /// FixedUpdate method called at fixed time intervals for this state.
    /// </summary>
    public virtual void StateFixedUpdate()
    {
        if (shouldLogUpdate)
            Debug.Log("FixedUpdate");
    }

    /// <summary>
    /// LateUpdate method called after all Update functions have been called for this state. 
    /// </summary>
    public virtual void StateLateUpdate()
    {
        if (shouldLogUpdate)
            Debug.Log("LateUpdate");
    }

    /// <summary>
    /// Method called when exiting this state.
    /// </summary>
    public virtual void Exit()
    {
        if (shouldLogUpdate)
            Debug.Log("Exit");
    }

    /// <summary>
    /// Calls the onFinished event for transitioning to a specific state type T.
    /// </summary>
    /// <typeparam name="T">Type of state to transition to.</typeparam>
    public void CallChangeState<T>() where T : State
    {
        foreach(State state in nextStates)
        {
            if(state as T)
            {
                onFinished?.Invoke(state);
            }
        }
    }
}
