using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Interact playerInteract)
    {
        if(!HasKitchenObject())
        {
            if(playerInteract.HasKitchenObject())
            {
                playerInteract.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if(!playerInteract.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(playerInteract);
            }
        }
    }
}
