using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// Handles player interaction with kitchen counters and objects.
/// </summary>
public class Interact : MonoBehaviour, IKitchenObjectParent
{
    [Tooltip("Reference to the game manager managing game state.")]
    [SerializeField] private KitchenGameManager gameManager;
    
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    [Tooltip("Transform point where kitchen objects will adquire when picked.")]
    [SerializeField] private Transform kitchenObjectHoldPoint;

    public static event EventHandler OnPickedSomething;

    private void Update()
    {
        SelectCounter();
    }

    /// <summary>
    /// Selects the kitchen counter in front of the player within a certain distance and toggles its higlighting.
    /// </summary>
    private void SelectCounter()
    {
        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    selectedCounter?.ToggleHighlight();
                    baseCounter.ToggleHighlight();
                    selectedCounter = baseCounter;
                }
            }
            else
            {
                selectedCounter?.ToggleHighlight();
                selectedCounter = null;
            }
        }
        else
        {
            selectedCounter?.ToggleHighlight();
            selectedCounter = null;
        }
    }

    /// <summary>
    /// Handles primary interaction with the selected kitchen counter.
    /// </summary>
    public void HandleInteraction()
    {
        if(!gameManager.IsGamePlaying())
            return;

        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    /// <summary>
    /// Handles alternate interaction with the selected kitchen counter.
    /// </summary>
    public void HandleInteractionAlternate()
    {
        if (!gameManager.IsGamePlaying())
            return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    /// <summary>
    /// Returns the transform point where kitchen objects whill adquire when picked.
    /// </summary>
    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    /// <summary>
    /// Sets the currently held kitchen object and invokes the event indicating something was picked up.
    /// </summary>
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Retrieves the currently held kitchen object.
    /// </summary>
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    /// <summary>
    /// Clears the currently held kitchen object.
    /// </summary>
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    /// <summary>
    /// Checks if the player currently holds a kitchen object.
    /// </summary>
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
