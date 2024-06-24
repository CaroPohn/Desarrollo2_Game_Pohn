using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterFSM : FSM
{
    [SerializeField] private StoveCounter stoveCounter;


    public override void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
            currentState.onStateChanged -= ChangeState;
        }

        currentState = newState;

        currentState.onStateChanged += ChangeState;

        currentState.Enter(states, stoveCounter);
    }

    protected override void Update()
    {
        if(stoveCounter.HasKitchenObject())
        {
            base.Update();
        }
    }

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
