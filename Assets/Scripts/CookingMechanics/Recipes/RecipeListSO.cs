using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject containing a list of recipes.
/// </summary>
[CreateAssetMenu()]
public class RecipeListSO : ScriptableObject
{
    [Tooltip("List of recipes.")]
    public List<RecipeSO> recipeSOList;
}
