using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class KitchenObjectFactory : MonoBehaviour
{
    private static KitchenObjectFactory instance = null;

    [SerializeField] private KitchenObject defaultPrefab;

    private ObjectPool<KitchenObject> pool;

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

    private void OnDestroyPoolObject(KitchenObject kitchenObject)
    {
        Destroy(kitchenObject.gameObject);
    }

    private void OnRelease(KitchenObject kitchenObject)
    {
        kitchenObject.gameObject.SetActive(false);
        kitchenObject.OnKitchenObjectDestroy.RemoveListener(instance.OnRelease);
    }

    private void OnGet(KitchenObject kitchenObject)
    {
        kitchenObject.gameObject.SetActive(true);
    }

    private KitchenObject CreateFunc()
    {
        return Instantiate(defaultPrefab);
    }

    public KitchenObject GetKitchenObjectCrafted(KitchenObjectSO KOConfig)
    {
        var newIngredient = pool.Get();

        newIngredient = ConfigureKitchenObject(KOConfig, newIngredient);

        return newIngredient;
    }

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
