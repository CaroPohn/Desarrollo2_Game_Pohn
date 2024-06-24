using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FryingState", menuName = "StoveCounter/FryingState", order = 0)]
public class FryingState : State
{
    private float fryingTimer;

    StoveCounter stoveCounter;

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
    }

    public override void StateUpdate()
    {
        fryingTimer += Time.deltaTime;

        if (fryingTimer > stoveCounter.FryingRecipe.fryingTimerMax)
        {
            stoveCounter.GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(stoveCounter.FryingRecipe.output, stoveCounter);

            CallChangeState<FriedState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
