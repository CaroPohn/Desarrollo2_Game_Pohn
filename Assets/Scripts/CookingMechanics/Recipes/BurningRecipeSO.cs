using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject defining a burning recipe for cooking.
/// </summary>
[CreateAssetMenu]
public class BurningRecipeSO : ScriptableObject
{
    [Tooltip("Input kitchen object required for the recipe.")]
    public KitchenObjectSO input;

    [Tooltip("Output kitchen object produced after burning.")]
    public KitchenObjectSO output;

    [Tooltip("Duration of burning required for the recipe.")]
    public float burningTimer;
}
