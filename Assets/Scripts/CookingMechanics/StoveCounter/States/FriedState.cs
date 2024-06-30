using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FriedState", menuName = "StoveCounter/FriedState", order = 0)]
public class FriedState : State
{
    private float burningTimer;

    StoveCounter stoveCounter;

    private bool isBurned = false;

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

    public override void Exit()
    {
        isBurned = false;
    }
}
