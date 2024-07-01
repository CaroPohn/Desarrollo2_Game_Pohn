using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject defining a cutting recipe for preparing ingredients.
/// </summary>
[CreateAssetMenu]
public class CuttingRecipeSO : ScriptableObject
{
    [Tooltip("Input kitchen object required for the recipe.")]
    public KitchenObjectSO input;

    [Tooltip("Output kitchen object produced after cutting.")]
    public KitchenObjectSO output;

    [Tooltip("Maximum number of cuts required to complete cutting.")]
    public int cuttingProgressMax;
}    

