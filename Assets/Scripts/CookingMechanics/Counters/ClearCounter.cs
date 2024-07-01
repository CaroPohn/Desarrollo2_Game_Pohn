using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    /// <summary>
    /// Interacts with the counter, allowing the player to place or retrieve kitchen objects.
    /// </summary>
    /// <param name="playerInteract">The object interacting with the counter.</param>
    public override void Interact(Interact playerInteract)
    {
        if(!HasKitchenObject())
        {
            if(playerInteract.HasKitchenObject())
            {
                playerInteract.GetKitchenObject().SetKitchenObjectParent(this);
                kitchenObject.transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            if(playerInteract.HasKitchenObject())
            {
                if(playerInteract.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if(plateKitchenObject.TryAddIngredient(playerInteract.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            playerInteract.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(playerInteract);
            }
        }
    }
}
