using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Interact : MonoBehaviour, IKitchenObjectParent
{
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private void Update()
    {
        SelectCounter();
    }

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

    public void HandleInteraction()
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    public void HandleInteractionAlternate()
    {
        if(selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
