using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public override void Interact(Interact playerInteract)
    {
        if (!HasKitchenObject())
        {
            if (playerInteract.HasKitchenObject())
            {
                playerInteract.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (!playerInteract.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(playerInteract);
            }
        }
    }
}
