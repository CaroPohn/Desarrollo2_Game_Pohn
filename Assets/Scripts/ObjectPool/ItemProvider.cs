using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProvider : MonoBehaviour
{
    private UnityEngine.Pool.ObjectPool<KitchenObject> pool;

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;

    public int amountToPool;

    void Start()
    {
        pooledObjects = new List<GameObject>();

        GameObject emptyObject;
        for (int i = 0; i < amountToPool; i++)
        {
            emptyObject = Instantiate(objectToPool);
            emptyObject.SetActive(false);
            pooledObjects.Add(emptyObject);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        GameObject emptyObject;

        emptyObject = Instantiate(objectToPool);
        emptyObject.SetActive(false);
        pooledObjects.Add(emptyObject);

        return emptyObject;
    }
}
