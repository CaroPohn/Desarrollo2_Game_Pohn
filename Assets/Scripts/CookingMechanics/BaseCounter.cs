using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] protected GameObject highlightedCounter;
    protected bool isHighlighted = false;

    protected KitchenObject kitchenObject;
    [SerializeField] protected KitchenObjectSO kitchenObjectSO;
    [SerializeField] public Transform counterTopPoint;

    public virtual void Interact(Interact playerInteract)
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate(Interact playerInteract)
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void ToggleHighlight()
    {
        isHighlighted = !isHighlighted;
        highlightedCounter.SetActive(isHighlighted);
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
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
