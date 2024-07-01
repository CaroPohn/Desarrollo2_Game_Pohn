using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCounter : BaseCounter
{

    [Tooltip("Reference to the Delivery Manager to handle recipe deliveries.")]
    [SerializeField] DeliveryManager deliveryManager;

    /// <summary>
    /// Interacts with the order counter, allowing the player to deliver a plate of food if they are holding one.
    /// </summary>
    /// <param name="playerInteract">The object interacting with the counter.</param>
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
