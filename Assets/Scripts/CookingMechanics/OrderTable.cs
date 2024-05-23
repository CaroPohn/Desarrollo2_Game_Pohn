using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTable : BaseCounter
{
    public override void Interact(Interact playerInteract)
    {
        if (playerInteract.HasKitchenObject() && playerInteract.GetKitchenObject().kitchenObjectSO.objectName == kitchenObjectSO.objectName)
        {
            Destroy(playerInteract.GetKitchenObject().gameObject);
            playerInteract.SetKitchenObject(null);
        }
    }
}
