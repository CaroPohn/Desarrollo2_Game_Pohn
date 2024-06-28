using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] KitchenObjectSO plateKitchenObjectSO;

    [SerializeField] private List<GameObject> plateVisualGameObjectList = new List<GameObject>();

    private float plateOffsetY = 0.15f;

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;

        for (int i = 0; i < platesCounter.platesSpawnedAmountMax; i++)
        {
            plateVisualGameObjectList[i].transform.position = new Vector3(counterTopPoint.position.x, counterTopPoint.position.y + plateOffsetY * i, counterTopPoint.position.z);
            plateVisualGameObjectList[i].SetActive(false);
        }
    }
    private void PlatesCounter_OnPlateSpawned()
    {
        plateVisualGameObjectList[platesCounter.platesSpawnedAmount - 1].SetActive(true);
    }

    private void PlatesCounter_OnPlateRemoved()
    {
        plateVisualGameObjectList[platesCounter.platesSpawnedAmount].SetActive(false);
    }

}
