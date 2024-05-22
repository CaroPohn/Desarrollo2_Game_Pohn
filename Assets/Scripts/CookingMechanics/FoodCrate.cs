using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCrate : BaseCounter, IKitchenObjectParent
{
    public override void Interact(Interact playerInteract)
    {
        if (!HasKitchenObject())
        {
            if(!playerInteract.HasKitchenObject())
            {
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(playerInteract);
            }

        }
    }
}
