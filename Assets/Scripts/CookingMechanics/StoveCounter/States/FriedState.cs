using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FriedState", menuName = "StoveCounter/FriedState", order = 0)]
public class FriedState : State
{
    private float burningTimer;

    StoveCounter stoveCounter;

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
    }

    public override void StateUpdate()
    {
        burningTimer += Time.deltaTime;

        if (burningTimer > stoveCounter.BurningRecipe.burningTimer)
        {
            stoveCounter.GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(stoveCounter.BurningRecipe.output, stoveCounter);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}