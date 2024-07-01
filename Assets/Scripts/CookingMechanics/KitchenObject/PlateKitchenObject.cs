using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a plate kitchen object that can hold multiple ingredients.
/// </summary>
public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientsAddedEventArgs> OnIngredientsAdded;

    /// <summary>
    /// Event arguments for the OnIngredientsAdded event.
    /// </summary>
    public class OnIngredientsAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    private List<KitchenObjectSO> kitchenObjectSOList = new List<KitchenObjectSO>();

    [Tooltip("List of valid kitchen object ScriptableObjects that can be added to the plate.")]
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList = new List<KitchenObjectSO>();

    /// <summary>
    /// Tries to add an ingredient to the plate.
    /// </summary>
    /// <param name="kitchenObjectSO">The KitchenObjectSO of the ingredient to add.</param>
    /// <returns>True if the ingredient was successfully added, false otherwise.</returns>
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))
            return false;

        if (kitchenObjectSOList.Contains(kitchenObjectSO))
            return false;
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientsAdded?.Invoke(this, new OnIngredientsAddedEventArgs { kitchenObjectSO = kitchenObjectSO });
            return true;
        }
    }

    /// <summary>
    /// Retrieves the list of kitchen object ScriptableObjects currently on the plate.
    /// </summary>
    /// <returns>The list of kitchen object ScriptableObjects.</returns>
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
