using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject defining a recipe with a list of kitchen objects required.
/// </summary>
[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    [Tooltip("List of kitchen objects required for the recipe.")]
    public List<KitchenObjectSO> kitchenObjectsSOList;

    [Tooltip("Name of the recipe.")]
    public string recipeName;
}
