using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class IngredientFactory : MonoBehaviour
{
    private static IngredientFactory instance = null;

    [SerializeField] private KitchenObjectSO[] ingredients;

    [SerializeField] private KitchenObject defaultPrefab;

    private ObjectPool<KitchenObject> pool;

    public static IngredientFactory Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<IngredientFactory>();

            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);

        instance = this;

        pool = new ObjectPool<KitchenObject>(CreateFunc, OnGet, OnRelease, OnDest, false, 10, 20);
    }

    private void OnDest(KitchenObject kitchenObject)
    {
        Destroy(kitchenObject.gameObject);
    }

    private void OnRelease(KitchenObject kitchenObject)
    {
        kitchenObject.gameObject.SetActive(false);
        kitchenObject.OnObjectDestroy.RemoveListener(instance.OnRelease);
    }

    private void OnGet(KitchenObject kitchenObject)
    {
        kitchenObject.gameObject.SetActive(true);
    }

    private KitchenObject CreateFunc()
    {
        return Instantiate(defaultPrefab);
    }

    public KitchenObject GetIngredient(KitchenObjectSO KOConfig)
    {
        var newIngredient = pool.Get();

        newIngredient = ConfigureIngredient(KOConfig, newIngredient);

        return newIngredient;
    }

    public KitchenObject ConfigureIngredient(KitchenObjectSO KOConfig, KitchenObject kitchenObject)
    {
        kitchenObject.transform.localPosition = Vector3.zero;
        kitchenObject.transform.position = Vector3.zero;
        kitchenObject.transform.rotation = Quaternion.identity;

        kitchenObject.kitchenObjectSO = KOConfig;
        kitchenObject.GetComponentInChildren<MeshFilter>().mesh = KOConfig.mesh;
        kitchenObject.GetComponentInChildren<MeshRenderer>().material = KOConfig.material;

        kitchenObject.OnObjectDestroy.AddListener(instance.OnRelease);

        return kitchenObject;
    }
}
