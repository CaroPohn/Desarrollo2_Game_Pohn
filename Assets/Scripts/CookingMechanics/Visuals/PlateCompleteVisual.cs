using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the visual representation of a completed plate based on added ingredients.
/// </summary>
public class PlateCompleteVisual : MonoBehaviour
{
    /// <summary>
    /// Represents a mapping between KitchenObjectSO and its corresponding GameObject for visual representation.
    /// </summary>
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        [Tooltip("The KitchenObjectSO this GameObject represents.")]
        public KitchenObjectSO kitchenObjectSO;

        [Tooltip("The GameObject that visually represents the KitchenObjectSO.")]
        public GameObject gameObject;
    }

    [Tooltip("The PlateKitchenObject instance to monitor for ingredient additions.")]
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    [Tooltip("List of KitchenObjectSOs and their corresponding GameObjects for visual representation.")]
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectsSOGameObjectList = new List<KitchenObjectSO_GameObject>();

    private void Start()
    {
        plateKitchenObject.OnIngredientsAdded += PlateKitchenObject_OnIngredientsAdded;
    }

    /// <summary>
    /// Activates the GameObject associated with the added ingredient.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">Event arguments containing the added KitchenObjectSO.</param>
    private void PlateKitchenObject_OnIngredientsAdded(object sender, PlateKitchenObject.OnIngredientsAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectsSOGameObjectList)
        {
            if(kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
