using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress 
{
    private int cuttingProgress;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event Action OnCut;

    [SerializeField] private float cutCooldown;

    private float lastCutTime;

    private void Start()
    {
        lastCutTime = -cutCooldown;
    }

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

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    cuttingProgress = 0;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax});
                }
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

    /// <summary>
    /// When a player interacts without leaving an ingredient, like cutting the vegetable.
    /// </summary>
    /// <param name="playerInteract"></param>
    public override void InteractAlternate(Interact playerInteract)
    {
        if (Time.time - lastCutTime < cutCooldown)
            return;

        if (!HasKitchenObject() || !HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
            return;

        lastCutTime = Time.time;

        cuttingProgress++;

        OnCut?.Invoke();

        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax });

        if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
        {
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();

            KitchenObject kitchenObject = IngredientFactory.Instance.GetIngredient(outputKitchenObjectSO);

            KitchenObject.SpawnKitchenObject(kitchenObject, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        if(cuttingRecipeSO != null)
        { 
            return cuttingRecipeSO.output; 
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}
