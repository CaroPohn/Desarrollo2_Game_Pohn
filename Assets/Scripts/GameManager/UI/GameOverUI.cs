using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private KitchenGameManager kitchenGameManager;
    [SerializeField] private DeliveryManager deliveryManager;

    [SerializeField] private Image firstStar;
    [SerializeField] private Image secondStar;
    [SerializeField] private Image thirdStar;

    [SerializeField] private StarsNecessaryOrdersSO starsNecessaryOrdersSO;

    private void Start()
    {
        kitchenGameManager.OnStateChanged += KitchenGameManager_OnStateChanged;

        Hide();
    }

    private void OnDestroy()
    {
        kitchenGameManager.OnStateChanged -= KitchenGameManager_OnStateChanged;
    }

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

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

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
