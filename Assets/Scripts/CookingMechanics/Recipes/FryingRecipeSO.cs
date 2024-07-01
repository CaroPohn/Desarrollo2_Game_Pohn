using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject defining a frying recipe for cooking.
/// </summary>
[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    [Tooltip("Input kitchen object required for the recipe.")]
    public KitchenObjectSO input;

    [Tooltip("Output kitchen object produced after frying.")]
    public KitchenObjectSO output;

    [Tooltip("Maximum frying duration required for the recipe.")]
    public float fryingTimerMax;
}
