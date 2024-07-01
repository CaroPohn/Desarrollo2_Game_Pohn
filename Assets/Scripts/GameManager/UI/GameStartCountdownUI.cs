using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// UI component that displays the countdown to game start.
/// </summary>
public class GameStartCountdownUI : MonoBehaviour
{
    [Tooltip("Text component used to display the countdown.")]
    [SerializeField] private TextMeshProUGUI countdownText;

    [Tooltip("Reference to the GameManager managing game state.")]
    [SerializeField] private KitchenGameManager kitchenGameManager;

    private void Start()
    {
        kitchenGameManager.OnStateChanged += KitchenGameManager_OnStateChanged;

        Hide();
    }

    /// <summary>
    /// Subscribes to the game state change event to control visibility based on countdown state.
    /// </summary>
    private void KitchenGameManager_OnStateChanged()
    {
        if(kitchenGameManager.IsCountdownToStartActive())
            Show();
        else
            Hide();
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(kitchenGameManager.GetCountdownToStartTimer()).ToString();
    }

    /// <summary>
    /// Shows the countdown UI.
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the countdown UI.
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
