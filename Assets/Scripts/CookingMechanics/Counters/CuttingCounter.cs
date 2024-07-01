using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress 
{
    private int cuttingProgress;

    [Tooltip("Array of cutting recipes.")]
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event Action OnCut;

    public static event EventHandler OnAnyCut;

    [Tooltip("Cooldown time between cuts.")]
    [SerializeField] private float cutCooldown;

    private float lastCutTime;

    private void Start()
    {
        lastCutTime = -cutCooldown;
    }

    /// <summary>
    /// Interacts with the counter, allowing the player to place or retrieve kitchen objects if the object is valid based on a cutting recipe.
    /// </summary>
    /// <param name="playerInteract">The object interacting with the counter.</param>
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
            if(playerInteract.HasKitchenObject())
            {
                if (playerInteract.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(playerInteract);
            }
        }
    }

    /// <summary>
    /// Allows the player to cut the kitchen object on the counter if a valid object is placed and cooldown has passed.
    /// </summary>
    /// <param name="playerInteract">The object interacting with the counter.</param>
    public override void InteractAlternate(Interact playerInteract)
    {
        if (Time.time - lastCutTime < cutCooldown)
            return;

        if (!HasKitchenObject() || !HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
            return;

        lastCutTime = Time.time;

        cuttingProgress++;

        OnCut?.Invoke();
        OnAnyCut?.Invoke(this, EventArgs.Empty);

        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax });

        if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
        {
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();

            KitchenObject kitchenObject = KitchenObjectFactory.Instance.GetKitchenObjectCrafted(outputKitchenObjectSO);

            KitchenObject.SetParentSpawnedKitchenObject(kitchenObject, this);
        }
    }

    /// <summary>
    /// Checks if there is a cutting recipe for the given input kitchen object.
    /// </summary>
    /// <param name="inputKitchenObjectSO">The kitchen object to check for a recipe.</param>
    /// <returns>true if a recipe exists; otherwise, false.</returns>
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;
    }

    /// <summary>
    /// Gets the output kitchen object for a given input kitchen object based on the cutting recipe.
    /// </summary>
    /// <param name="inputKitchenObjectSO">The kitchen object to get the output for.</param>
    /// <returns>The output kitchen object.</returns>
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

    /// <summary>
    /// Gets the cutting recipe for a given input kitchen object.
    /// </summary>
    /// <param name="inputKitchenObjectSO">The kitchen object to get the recipe.</param>
    /// <returns>The cutting recipe if found; otherwise, null.</returns>
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
