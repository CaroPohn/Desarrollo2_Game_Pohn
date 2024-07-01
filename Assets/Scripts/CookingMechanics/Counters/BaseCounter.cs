using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [Tooltip("Object that represents the highlighted counter.")]
    [SerializeField] protected GameObject highlightedCounter;

    protected bool isHighlighted = false;

    protected KitchenObject kitchenObject;

    [Tooltip("Transform indicating the top point of the counter.")]
    [SerializeField] public Transform counterTopPoint;

    public static event EventHandler OnAnyObjectPlacedHere;

    /// <summary>
    /// Logic for interacting with the counter. This method can be overridden by derived classes.
    /// </summary>
    /// <param name="playerInteract">The object interacting with the counter.</param>
    public virtual void Interact(Interact playerInteract)
    {
        Debug.Log("Interact");   
    }

    /// <summary>
    /// Alternative interaction logic for the counter. This method can be overridden by derived classes.
    /// </summary>
    /// <param name="playerInteract">The object interacting with the counter.</param>
    public virtual void InteractAlternate(Interact playerInteract)
    {
        Debug.Log("InteractAlternate");
    }

    /// <summary>
    /// Toggles the highlight state of the counter.
    /// </summary>
    public virtual void ToggleHighlight()
    {
        isHighlighted = !isHighlighted;
        highlightedCounter.SetActive(isHighlighted);
    }

    /// <summary>
    /// Gets the Transform that the kitchen object is going to adquire in the counter.
    /// </summary>
    /// <returns>The Transform of the counter top point.</returns>
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    /// <summary>
    /// Sets the kitchen object on the counter.
    /// </summary>
    /// <param name="kitchenObject">The kitchen object to set.</param>
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null )
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets the kitchen object currently on the counter.
    /// </summary>
    /// <returns>The kitchen object on the counter.</returns>
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    /// <summary>
    /// Clears the kitchen object from the counter.
    /// </summary>
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    /// <summary>
    /// Checks if there is a kitchen object on the counter.
    /// </summary>
    /// <returns>true if there is a kitchen object on the counter; otherwise, false.</returns>
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
