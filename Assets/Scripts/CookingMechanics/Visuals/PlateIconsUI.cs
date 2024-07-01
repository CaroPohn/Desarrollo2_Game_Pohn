using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the UI representation of kitchen object icons on a plate.
/// </summary>
public class PlateIconsUI : MonoBehaviour
{
    [Tooltip("The PlateKitchenObject to observe for ingredient additions.")]
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    [Tooltip("The template for the icon that represents each kitchen object.")]
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientsAdded += PlateKitchenObject_OnIngredientsAdded;
    }

    /// <summary>
    /// Event handler for when ingredients are added to the plate.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Event arguments containing the added kitchen object.</param>
    private void PlateKitchenObject_OnIngredientsAdded(object sender, PlateKitchenObject.OnIngredientsAddedEventArgs e)
    {
        UpdateVisual();
    }

    /// <summary>
    /// Updates the visual representation of kitchen object icons on the plate.
    /// </summary>
    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if(child == iconTemplate) 
                continue;

            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
