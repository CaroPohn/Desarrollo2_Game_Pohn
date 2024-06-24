using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    protected List<State> nextStates;

    public event Action<State> onStateChanged;

    public virtual void Enter(List<State> possibleStates, params object[] objects)
    {
        nextStates = possibleStates;
    }
    public virtual void StateUpdate()
    {
        Debug.Log("Update");
    }
    public virtual void StateFixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }
    public virtual void StateLateUpdate()
    {
        Debug.Log("LateUpdate");
    }
    public virtual void Exit()
    {
        Debug.Log("Exit");
    }

    public void CallChangeState<T>() where T : State
    {
        foreach(State state in nextStates)
        {
            if(state as T)
            {
                onStateChanged?.Invoke(state);
            }
        }
    }
}
