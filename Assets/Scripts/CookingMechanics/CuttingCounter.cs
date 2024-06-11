using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    private int cuttingProgress;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Interact playerInteract)
    {
        if (!HasKitchenObject())
        {
            if (playerInteract.HasKitchenObject())
            {
                playerInteract.GetKitchenObject().SetKitchenObjectParent(this);
                kitchenObject.transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            if (!playerInteract.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(playerInteract);
            }
        }
    }

    public override void InteractAlternate(Interact playerInteract)
    {
        if (HasKitchenObject())
        {
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }

        return null;
    }
}
