using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State representing the idle state of a stove counter, where no active process is ongoing.
/// </summary>
[CreateAssetMenu(fileName = "IdleState", menuName = "StoveCounter/IdleState", order = 0)]
public class IdleState : State
{
    /// <summary>
    /// Initializes the idle state with possible states and optional objects.
    /// </summary>
    /// <param name="possibleStates">List of possible states.</param>
    /// <param name="objects">Additional objects to initialize the state.</param>
    public override void Enter(List<State> possibleStates, params object[] objects)
    {
        base.Enter(possibleStates);
    }

    /// <summary>
    /// Calls the base State Update.
    /// </summary>
    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    /// <summary>
    /// Calls the base State Exit.
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}
