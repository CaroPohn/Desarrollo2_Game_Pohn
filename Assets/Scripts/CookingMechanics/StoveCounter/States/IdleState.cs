using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "StoveCounter/IdleState", order = 0)]
public class IdleState : State
{
    public override void Enter(List<State> possibleStates, params object[] objects)
    {
        base.Enter(possibleStates);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
