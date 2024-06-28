using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCrate : BaseCounter, IKitchenObjectParent
{
    [SerializeField] protected KitchenObjectSO kitchenObjectSO;

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
