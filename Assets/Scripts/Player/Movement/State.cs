using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public event Action<State> onFinished;
    [SerializeField] private bool shouldLogUpdate = false;
    [SerializeField] private ShouldLog shouldLog = ShouldLog.None;

    public virtual void Enter(List<State> possibleStates, params object[] objects)
    {
        nextStates = possibleStates;
    }
    public virtual void StateUpdate()
    {
        if(shouldLogUpdate)
            Debug.Log("Update");
    }
    public virtual void StateFixedUpdate()
    {
        //TOOD: Add the rest of the ifs
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
                onFinished?.Invoke(state);
            }
        }
    }
}
