using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI manager for displaying game over screen.
/// </summary>
public class GameOverUI : MonoBehaviour
{
    [Tooltip("Text component displaying the number of recipes delivered.")]
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    [Tooltip("Reference to the KitchenGameManager managing game states.")]
    [SerializeField] private KitchenGameManager kitchenGameManager;

    [Tooltip("Reference to the DeliveryManager handling recipe deliveries.")]
    [SerializeField] private DeliveryManager deliveryManager;

    [Tooltip("Image displaying the first star for performance.")]
    [SerializeField] private Image firstStar;

    [Tooltip("Image displaying the second star for performance.")]
    [SerializeField] private Image secondStar;

    [Tooltip("Image displaying the third star for performance.")]
    [SerializeField] private Image thirdStar;

    [Tooltip("Scriptable object containing necessary orders for star ratings.")]
    [SerializeField] private StarsNecessaryOrdersSO starsNecessaryOrdersSO;

    private void Start()
    {
        kitchenGameManager.OnStateChanged += KitchenGameManager_OnStateChanged;

        firstStar.gameObject.SetActive(false);
        secondStar.gameObject.SetActive(false);
        thirdStar.gameObject.SetActive(false);

        Hide();
    }

    private void OnDestroy()
    {
        kitchenGameManager.OnStateChanged -= KitchenGameManager_OnStateChanged;
    }

    /// <summary>
    /// Handles changes in game state to show or hide the game over UI.
    /// </summary>
    private void KitchenGameManager_OnStateChanged()
    {
        if (kitchenGameManager.IsGameOver())
        { 
            Show(); 
            recipesDeliveredText.text = deliveryManager.GetSuccesfulRecipesAmount().ToString();
            StarsResults();
        }
        else
        {
            Hide();
        }
    }

    /// <summary>
    /// Displays the game over UI.
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the game over UI.
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Determines and displays the stars based on successful recipe deliveries.
    /// </summary>
    private void StarsResults()
    {
        if(deliveryManager.GetSuccesfulRecipesAmount() >= starsNecessaryOrdersSO.firstStarOrders)
        {
            firstStar.gameObject.SetActive(true);
        }

        if (deliveryManager.GetSuccesfulRecipesAmount() >= starsNecessaryOrdersSO.secondStarOrders)
        {
            secondStar.gameObject.SetActive(true);
        }

        if (deliveryManager.GetSuccesfulRecipesAmount() >= starsNecessaryOrdersSO.thirdStarOrders)
        {
            thirdStar.gameObject.SetActive(true);
        }
    }
}
