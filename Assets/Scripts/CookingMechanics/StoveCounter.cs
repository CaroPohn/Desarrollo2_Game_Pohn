using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    public FryingRecipeSO FryingRecipe { get { return fryingRecipeSO; } set { fryingRecipeSO = value; } }
    public BurningRecipeSO BurningRecipe { get { return burningRecipeSO; } set { burningRecipeSO = value; } }

    [SerializeField] private StoveCounterFSM fsm;

    public override void Interact(Interact playerInteract)
    {
        if (!HasKitchenObject())
        {
            if (playerInteract.HasKitchenObject())
            {
                if (HasRecipeWithInput(playerInteract.GetKitchenObject().GetKitchenObjectSO()))
                {
                    playerInteract.GetKitchenObject().SetKitchenObjectParent(this);
                    kitchenObject.transform.rotation = Quaternion.identity;

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().kitchenObjectSO);

                    fsm.OnStoveInteract<FryingState>();
                }
            }
        }
        else
        {
            if (!playerInteract.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(playerInteract);
                fsm.OnStoveInteract<IdleState>();
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }

        return null;
    }

    public BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }

        return null;
    }
}
