using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class KitchenObjectFactory : MonoBehaviour
{
    private static KitchenObjectFactory instance = null;

    [Tooltip("Prefab for default kitchen objects.")]
    [SerializeField] private KitchenObject defaultPrefab;

    [Tooltip("Prefab for default plate kitchen objects.")]
    [SerializeField] private PlateKitchenObject defaultPlatePrefab;

    private ObjectPool<KitchenObject> pool;

    /// <summary>
    /// Singleton instance property to access the KitchenObjectFactory instance.
    /// </summary>
    public static KitchenObjectFactory Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<KitchenObjectFactory>();

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);

        instance = this;
    }

    private void Start()
    {
        pool = new ObjectPool<KitchenObject>(CreateFunc, OnGet, OnRelease, OnDestroyPoolObject, false, 10, 20);
    }

    /// <summary>
    /// Destroys the GameObject associated with the KitchenObject when it's released from the pool.
    /// </summary>
    /// <param name="kitchenObject">The KitchenObject to destroy.</param>
    private void OnDestroyPoolObject(KitchenObject kitchenObject)
    {
        Destroy(kitchenObject.gameObject);
    }

    /// <summary>
    /// Actions performed when a KitchenObject is released back into the pool.
    /// Deactivates the GameObject and removes the listener for OnKitchenObjectDestroy event.
    /// </summary>
    /// <param name="kitchenObject">The KitchenObject being released.</param>
    private void OnRelease(KitchenObject kitchenObject)
    {
        kitchenObject.gameObject.SetActive(false);
        kitchenObject.OnKitchenObjectDestroy.RemoveListener(instance.OnRelease);
    }

    /// <summary>
    /// Actions performed when a KitchenObject is retrieved from the pool.
    /// Activates the GameObject.
    /// </summary>
    /// <param name="kitchenObject">The KitchenObject being retrieved.</param>
    private void OnGet(KitchenObject kitchenObject)
    {
        kitchenObject.gameObject.SetActive(true);
    }

    /// <summary>
    /// Function that creates a new instance of a KitchenObject.
    /// Used by the object pool to create new instances as needed.
    /// </summary>
    /// <returns>The newly created KitchenObject instance.</returns>
    private KitchenObject CreateFunc()
    {
        return Instantiate(defaultPrefab);
    }

    /// <summary>
    /// Creates and returns a new KitchenObject based on the provided KitchenObjectSO configuration.
    /// </summary>
    /// <param name="KOConfig">The KitchenObjectSO configuration to use.</param>
    /// <returns>The newly created KitchenObject.</returns>
    public KitchenObject GetKitchenObjectCrafted(KitchenObjectSO KOConfig)
    {
        KitchenObject newIngredient;

        if (KOConfig.objectName == "Plate")
        {
            newIngredient = Instantiate(defaultPlatePrefab);
        }
        else
        {
            newIngredient = pool.Get();
        }

        newIngredient = ConfigureKitchenObject(KOConfig, newIngredient);

        return newIngredient;
    }

    /// <summary>
    /// Configures a KitchenObject based on the provided KitchenObjectSO configuration.
    /// </summary>
    /// <param name="KOConfig">The KitchenObjectSO configuration to apply.</param>
    /// <param name="kitchenObject">The KitchenObject to configure.</param>
    /// <returns>The configured KitchenObject.</returns>
    public KitchenObject ConfigureKitchenObject(KitchenObjectSO KOConfig, KitchenObject kitchenObject)
    {
        kitchenObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        kitchenObject.kitchenObjectSO = KOConfig;
        kitchenObject.GetComponentInChildren<MeshFilter>().mesh = KOConfig.mesh;
        kitchenObject.GetComponentInChildren<MeshRenderer>().material = KOConfig.material;

        kitchenObject.OnKitchenObjectDestroy.AddListener(instance.OnRelease);

        return kitchenObject;
    }
}
