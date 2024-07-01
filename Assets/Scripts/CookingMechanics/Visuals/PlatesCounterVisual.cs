using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the visual representation of PlatesCounter.
/// </summary>
public class PlatesCounterVisual : MonoBehaviour
{
    [Tooltip("Reference to the PlatesCounter component.")]
    [SerializeField] private PlatesCounter platesCounter;

    [Tooltip("The point at the top of the counter where plates should be positioned.")]
    [SerializeField] private Transform counterTopPoint;

    [Tooltip("The kitchen object scriptable object representing a plate.")]
    [SerializeField] KitchenObjectSO plateKitchenObjectSO;

    [Tooltip("List of GameObjects representing visual plates on the counter.")]
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

    /// <summary>
    /// Event handler for when a plate is spawned.
    /// Activates the visual representation of the newly spawned plate.
    /// </summary>
    private void PlatesCounter_OnPlateSpawned()
    {
        plateVisualGameObjectList[platesCounter.platesSpawnedAmount - 1].SetActive(true);
    }

    /// <summary>
    /// Event handler for when a plate is removed.
    /// Deactivates the visual representation of the removed plate.
    /// </summary>
    private void PlatesCounter_OnPlateRemoved()
    {
        plateVisualGameObjectList[platesCounter.platesSpawnedAmount].SetActive(false);
    }

}
