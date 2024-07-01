using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the UI elements for displaying a single recipe in the delivery manager.
/// </summary>
public class DeliveryManagerSingleUI : MonoBehaviour
{
    [Tooltip("Text component displaying the recipe name.")]
    [SerializeField] private TextMeshProUGUI recipeNameText;

    [Tooltip("Container for displaying icons of kitchen objects.")]
    [SerializeField] private Transform iconContainer;

    [Tooltip("Template object for icon instantiation.")]
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets up the UI with information from the provided RecipeSO.
    /// </summary>
    /// <param name="recipeSO">RecipeSO containing the recipe information to display.</param>
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate)
                continue;

            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectsSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }

    }
}
