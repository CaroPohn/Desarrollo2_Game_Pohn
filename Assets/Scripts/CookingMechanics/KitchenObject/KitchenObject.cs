using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] public KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public UnityEvent<KitchenObject> OnKitchenObjectDestroy { get; set; } = new UnityEvent<KitchenObject>();

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

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();

        OnKitchenObjectDestroy.Invoke(this);
    }

    public static KitchenObject SetParentSpawnedKitchenObject(KitchenObject kitchenObject, IKitchenObjectParent kitchenObjectParent)
    {
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}
