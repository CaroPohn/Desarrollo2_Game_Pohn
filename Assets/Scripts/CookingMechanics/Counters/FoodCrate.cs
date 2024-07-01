using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCrate : BaseCounter, IKitchenObjectParent
{
    [Tooltip("The KitchenObject scriptable object representing the type of kitchen object contained in the crate.")]
    [SerializeField] protected KitchenObjectSO kitchenObjectSO;

    /// <summary>
    /// Interacts with the food crate, allowing the player to retrieve a kitchen object if they do not already have one.
    /// </summary>
    /// <param name="playerInteract">The object interacting with the crate.</param>
    public override void Interact(Interact playerInteract)
    {
        if (!HasKitchenObject())
        {
            if(!playerInteract.HasKitchenObject())
            {
                KitchenObject kitchenObject = KitchenObjectFactory.Instance.GetKitchenObjectCrafted(kitchenObjectSO);

                KitchenObject.SetParentSpawnedKitchenObject(kitchenObject, playerInteract);
            }
        }
    }
}
