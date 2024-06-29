using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientsAddedEventArgs> OnIngredientsAdded;
    public class OnIngredientsAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    private List<KitchenObjectSO> kitchenObjectSOList = new List<KitchenObjectSO>();

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList = new List<KitchenObjectSO>();

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
}
