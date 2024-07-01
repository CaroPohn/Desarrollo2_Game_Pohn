using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoveCounter : BaseCounter, IHasProgress
{
    [Tooltip("Array of frying recipes available for this stove counter.")]
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    [Tooltip("Array of burning recipes available for this stove counter.")]
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    public FryingRecipeSO FryingRecipe { get { return fryingRecipeSO; } set { fryingRecipeSO = value; } }
    public BurningRecipeSO BurningRecipe { get { return burningRecipeSO; } set { burningRecipeSO = value; } }

    [Tooltip("Finite State Machine controlling the stove counter's behavior.")]
    [SerializeField] private StoveCounterFSM fsm;

    [Tooltip("Event invoked when the stove is turned on.")]
    [SerializeField] public UnityEvent OnStoveOn;

    [Tooltip("Event invoked when the stove is turned off.")]
    [SerializeField] public UnityEvent OnStoveOff;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public float progress;

    /// <summary>
    /// Handles player interaction with the stove counter connecting with the FSM.
    /// </summary>
    /// <param name="playerInteract">Object representing the player's interaction.</param>
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

                    OnStoveOn?.Invoke();

                    fsm.OnStoveInteract<FryingState>();
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

                        OnStoveOff?.Invoke();

                        UpdateProgress(0f);

                        fsm.OnStoveInteract<IdleState>();
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(playerInteract);

                OnStoveOff?.Invoke();

                UpdateProgress(0f);

                fsm.OnStoveInteract<IdleState>();
            }
        }
    }

    /// <summary>
    /// Checks if there is a frying recipe that matches the provided input.
    /// </summary>
    /// <param name="inputKitchenObjectSO">Input kitchen object to check against recipes.</param>
    /// <returns>True if a frying recipe matches the input, false otherwise.</returns>
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        return fryingRecipeSO != null;
    }

    /// <summary>
    /// Retrieves the frying recipe that matches the provided input.
    /// </summary>
    /// <param name="inputKitchenObjectSO">Input kitchen object to find matching recipe.</param>
    /// <returns>The matching frying recipe, or null if no match is found.</returns>
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

    /// <summary>
    /// Retrieves the burning recipe that matches the provided input.
    /// </summary>
    /// <param name="inputKitchenObjectSO">Input kitchen object to find matching recipe.</param>
    /// <returns>The matching burning recipe, or null if no match is found.</returns>
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

    /// <summary>
    /// Updates the progress of the current stove operation.
    /// </summary>
    /// <param name="newProgress">The new progress value, normalized between 0 and 1.</param>
    public void UpdateProgress(float newProgress)
    {
        progress = newProgress;

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = progress});
    }
}
