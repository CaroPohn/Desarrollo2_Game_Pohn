using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
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

    private void KitchenGameManager_OnGameUnpaused()
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePaused()
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void GoBackMenu()
    {
        SceneLoader.Instance.ChangeScene("Main_Menu");
    }
}
