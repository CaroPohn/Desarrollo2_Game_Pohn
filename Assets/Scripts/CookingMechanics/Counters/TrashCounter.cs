using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;

    /// <summary>
    /// Interacts with the trash counter, destroying the kitchen object the player is holding and triggering the OnAnyObjectTrashed event.
    /// </summary>
    /// <param name="playerInteract">The object interacting with the counter.</param>
    public override void Interact(Interact playerInteract)
    {
        if(playerInteract.HasKitchenObject())
        {
            playerInteract.GetKitchenObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
