using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// State representing the frying process for a kitchen object on a stove counter.
/// </summary>
[CreateAssetMenu(fileName = "FryingState", menuName = "StoveCounter/FryingState", order = 0)]
public class FryingState : State
{
    private float fryingTimer;

    StoveCounter stoveCounter;

    /// <summary>
    /// Initializes the state with necessary parameters and resets the frying timer.
    /// </summary>
    /// <param name="possibleStates">List of possible states.</param>
    /// <param name="objects">Additional objects to initialize the state.</param>
    public override void Enter(List<State> possibleStates, params object[] objects)
    {
        base.Enter(possibleStates);
        
        foreach (object obj in objects)
        {
            if(obj as StoveCounter != null)
            {
                stoveCounter = obj as StoveCounter;
            }
        }

        fryingTimer = 0f;

        stoveCounter.UpdateProgress(fryingTimer / stoveCounter.FryingRecipe.fryingTimerMax);
    }

    /// <summary>
    /// Updates the frying process timer and checks if the frying is completed.
    /// </summary>
    public override void StateUpdate()
    {
        fryingTimer += Time.deltaTime;

        stoveCounter.UpdateProgress(fryingTimer / stoveCounter.FryingRecipe.fryingTimerMax);

        if (fryingTimer > stoveCounter.FryingRecipe.fryingTimerMax)
        {
            stoveCounter.GetKitchenObject().DestroySelf();

            KitchenObject kitchenObject = KitchenObjectFactory.Instance.GetKitchenObjectCrafted(stoveCounter.FryingRecipe.output);

            KitchenObject.SetParentSpawnedKitchenObject(kitchenObject, stoveCounter);

            CallChangeState<FriedState>();
        }
    }

    /// <summary>
    /// Calls the base State exit.
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}
