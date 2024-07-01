using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatesCounter : BaseCounter
{
    [Tooltip("Scriptable object representing the configuration of a plate to spawn.")]
    [SerializeField] KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;

    public int platesSpawnedAmount;
    public int platesSpawnedAmountMax = 4;

    public event Action OnPlateSpawned;
    public event Action OnPlateRemoved;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if (platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke();
            }
        }
    }

    /// <summary>
    /// Interacts with the plates counter, allowing the player to take a plate if available.
    /// </summary>
    /// <param name="playerInteract">The object interacting with the counter.</param>
    public override void Interact(Interact playerInteract)
    {
        if(!playerInteract.HasKitchenObject())
        {
            if(platesSpawnedAmount > 0)
            {
                platesSpawnedAmount--;

                KitchenObject kitchenObject = KitchenObjectFactory.Instance.GetKitchenObjectCrafted(plateKitchenObjectSO);
                kitchenObject.transform.localScale = new Vector3(1.35f, 1.10f, 1.35f);
                KitchenObject.SetParentSpawnedKitchenObject(kitchenObject, playerInteract);

                OnPlateRemoved?.Invoke();
                spawnPlateTimer = 0f;
            }
        }
    }
}
