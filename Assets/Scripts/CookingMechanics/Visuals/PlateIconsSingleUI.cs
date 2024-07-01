using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the visual representation of a single kitchen object icon on a plate.
/// </summary>
public class PlateIconsSingleUI : MonoBehaviour
{
    [Tooltip("The Image component used to display the kitchen object icon.")]
    [SerializeField] private Image image;

    /// <summary>
    /// Sets the kitchen object sprite to display on the icon.
    /// </summary>
    /// <param name="kitchenObjectSO">The KitchenObjectSO containing the sprite to display.</param>
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
    }
}
