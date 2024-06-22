using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Interact playerInteract)
    {
        if(playerInteract.HasKitchenObject())
        {
            playerInteract.GetKitchenObject().DestroySelf();
        }
    }
}
