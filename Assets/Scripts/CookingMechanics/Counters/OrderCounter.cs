using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCounter : BaseCounter
{
    [SerializeField] DeliveryManager deliveryManager;

    public override void Interact(Interact playerInteract)
    {
        if(playerInteract.HasKitchenObject())
        {
            if(playerInteract.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                deliveryManager.DeliverRecipe(plateKitchenObject);
                playerInteract.GetKitchenObject().DestroySelf();
            }
        }
    }
}
