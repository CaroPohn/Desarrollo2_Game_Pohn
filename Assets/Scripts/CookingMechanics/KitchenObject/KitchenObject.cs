using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents a kitchen object in the game, which can be managed by an IKitchenObjectParent.
/// </summary>
public class KitchenObject : MonoBehaviour
{
    [Tooltip("ScriptableObject defining properties of the kitchen object.")]
    [SerializeField] public KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    /// <summary>
    /// Retrieves the ScriptableObject defining properties of this kitchen object.
    /// </summary>
    /// <returns>The KitchenObjectSO associated with this object.</returns>
    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    /// <summary>
    /// Event triggered when this kitchen object is destroyed.
    /// </summary>
    public UnityEvent<KitchenObject> OnKitchenObjectDestroy { get; set; } = new UnityEvent<KitchenObject>();

    /// <summary>
    /// Sets the parent object that will manage this kitchen object.
    /// </summary>
    /// <param name="kitchenObjectParent">The parent object implementing IKitchenObjectParent.</param>
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if(kitchenObjectParent.HasKitchenObject())
        {
            Debug.Log("KitchenObjectParent alredy has a KitchenObject!");
        }
        else
        {
            kitchenObjectParent.SetKitchenObject(this);

            transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }
    }

    /// <summary>
    /// Retrieves the parent object currently managing this kitchen object.
    /// </summary>
    /// <returns>The IKitchenObjectParent managing this kitchen object.</returns>
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    /// <summary>
    /// Cleans this kitchen object from its parent and invokes an event to put it back in the KitchenObjectPool.
    /// </summary>
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();

        OnKitchenObjectDestroy.Invoke(this);
    }

    /// <summary>
    /// Sets the parent for a newly spawned kitchen object.
    /// </summary>
    /// <param name="kitchenObject">The kitchen object to set parent for.</param>
    /// <param name="kitchenObjectParent">The parent object implementing IKitchenObjectParent.</param>
    /// <returns>The kitchen object with its parent set.</returns>
    public static KitchenObject SetParentSpawnedKitchenObject(KitchenObject kitchenObject, IKitchenObjectParent kitchenObjectParent)
    {
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }

    /// <summary>
    /// Tries to retrieve if this kitchen object is a plate.
    /// </summary>
    /// <param name="plateKitchenObject">The plate kitchen object if found, otherwise null.</param>
    /// <returns>True if a plate kitchen object is found, false otherwise.</returns>
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
}
