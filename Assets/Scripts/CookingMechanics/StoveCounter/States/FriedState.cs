using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State representing the burning process for a kitchen object on a stove counter after being cooked.
/// </summary>
[CreateAssetMenu(fileName = "FriedState", menuName = "StoveCounter/FriedState", order = 0)]
public class FriedState : State
{
    private float burningTimer;

    StoveCounter stoveCounter;

    private bool isBurned = false;

    /// <summary>
    /// Initializes the state with necessary parameters and resets the burning timer.
    /// </summary>
    /// <param name="possibleStates">List of possible states.</param>
    /// <param name="objects">Additional objects to initialize the state.</param>
    public override void Enter(List<State> possibleStates, params object[] objects)
    {
        base.Enter(possibleStates);

        foreach (object obj in objects)
        {
            if (obj as StoveCounter != null)
            {
                stoveCounter = obj as StoveCounter;
            }
        }

        stoveCounter.BurningRecipe = stoveCounter.GetBurningRecipeSOWithInput(stoveCounter.GetKitchenObject().kitchenObjectSO);

        burningTimer = 0f;
        isBurned = false;

        stoveCounter.UpdateProgress(burningTimer / stoveCounter.BurningRecipe.burningTimer);
    }

    /// <summary>
    /// Updates the frying process timer and checks if the object is burned.
    /// </summary>
    public override void StateUpdate()
    {
        burningTimer += Time.deltaTime;

        stoveCounter.UpdateProgress(burningTimer / stoveCounter.BurningRecipe.burningTimer);

        if (burningTimer > stoveCounter.BurningRecipe.burningTimer && !isBurned)
        {
            stoveCounter.GetKitchenObject().DestroySelf();

            KitchenObject kitchenObject = KitchenObjectFactory.Instance.GetKitchenObjectCrafted(stoveCounter.BurningRecipe.output);

            KitchenObject.SetParentSpawnedKitchenObject(kitchenObject, stoveCounter);

            isBurned = true;

        }

        if(isBurned)
            stoveCounter.UpdateProgress(0f);
    }

    /// <summary>
    /// Resets the burned state when exiting the frying state.
    /// </summary>
    public override void Exit()
    {
        isBurned = false;
    }
}
