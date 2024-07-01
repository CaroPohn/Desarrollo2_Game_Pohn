using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for objects that can act as parents for kitchen objects in the game.
/// </summary>
public interface IKitchenObjectParent
{
    /// <summary>
    /// Returns the transform that a kitchen object should follow or be placed upon.
    /// </summary>
    /// <returns>The transform for kitchen object positioning.</returns>
    public Transform GetKitchenObjectFollowTransform();

    /// <summary>
    /// Sets the kitchen object that this parent will manage.
    /// </summary>
    /// <param name="kitchenObject">The kitchen object to set.</param>
    public void SetKitchenObject(KitchenObject kitchenObject);

    /// <summary>
    /// Retrieves the currently managed kitchen object in the parent.
    /// </summary>
    /// <returns>The currently managed kitchen object.</returns>
    public KitchenObject GetKitchenObject();

    /// <summary>
    /// Clears the currently managed kitchen object from this parent.
    /// </summary>
    public void ClearKitchenObject();

    /// <summary>
    /// Checks if this parent currently has a kitchen object.
    /// </summary>
    /// <returns>True if a kitchen object is currently managed, false otherwise.</returns>
    public bool HasKitchenObject();
}
