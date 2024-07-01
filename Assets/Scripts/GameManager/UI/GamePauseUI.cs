using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseUI : MonoBehaviour
{
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
}
