using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI manager for displaying pause menu during gameplay.
/// </summary>
public class GamePauseUI : MonoBehaviour
{
    [Tooltip("Button to go back to the main menu.")]
    [SerializeField] Button backMenuButton;

    private void Awake()
    {
        backMenuButton.onClick.AddListener(GoBackMenu);
    }

    private void Start()
    {
        KitchenGameManager.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        Hide();
    }

    private void OnDestroy()
    {
        KitchenGameManager.OnGamePaused -= KitchenGameManager_OnGamePaused;
        KitchenGameManager.OnGameUnpaused -= KitchenGameManager_OnGameUnpaused;
    }

    /// <summary>
    /// Responds to the event when the game is unpaused.
    /// </summary>
    private void KitchenGameManager_OnGameUnpaused()
    {
        Hide();
    }

    /// <summary>
    /// Responds to the event when the game is paused.
    /// </summary>
    private void KitchenGameManager_OnGamePaused()
    {
        Show();
    }

    /// <summary>
    /// Displays the pause menu UI.
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the pause menu UI.
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    private void GoBackMenu()
    {
        SceneLoader.Instance.ChangeScene("Main_Menu");
    }
}
