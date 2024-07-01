using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the UI elements for displaying multiple recipes in the delivery manager.
/// </summary>
public class DeliveryManagerUI : MonoBehaviour
{
    [Tooltip("Container for displaying recipe UI elements.")]
    [SerializeField] private Transform container;

    [Tooltip("Template object for recipe UI instantiation.")]
    [SerializeField] private Transform recipeTemplate;

    [Tooltip("Reference to the DeliveryManager that provides recipe data.")]
    [SerializeField] private DeliveryManager deliveryManager;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        deliveryManager.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        deliveryManager.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;

        UpdateVisual();
    }

    private void OnDestroy()
    {
        deliveryManager.OnRecipeSpawned -= DeliveryManager_OnRecipeSpawned;
        deliveryManager.OnRecipeCompleted -= DeliveryManager_OnRecipeCompleted;
    }

    /// <summary>
    /// Handler for when a recipe is completed in the delivery manager.
    /// Updates the visual display of recipes.
    /// </summary>
    private void DeliveryManager_OnRecipeCompleted()
    {
        UpdateVisual();
    }

    /// <summary>
    /// Handler for when a new recipe is spawned in the delivery manager.
    /// Updates the visual display of recipes.
    /// </summary>
    private void DeliveryManager_OnRecipeSpawned()
    {
        UpdateVisual();
    }

    /// <summary>
    /// Updates the visual display of recipes based on the current state of the delivery manager.
    /// Destroys existing recipe UI elements and instantiates new ones for each waiting recipe.
    /// </summary>
    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate)
                continue;

            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in deliveryManager.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
