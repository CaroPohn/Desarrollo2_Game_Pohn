using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerFSM : FSM
{
    [SerializeField] private KitchenGameManager kitchenGameManager;

    public override void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
            currentState.onFinished -= ChangeState;
        }

        currentState = newState;

        currentState.onFinished += ChangeState;

        currentState.Enter(states, kitchenGameManager);
    }

    protected override void Update()
    {
        base.Update();
    }

    public void OnGameChangeState<T>() where T : State
    {
        foreach (State state in states)
        {
            if (state as T)
            {
                ChangeState(state);
            }
        }
    }
}
