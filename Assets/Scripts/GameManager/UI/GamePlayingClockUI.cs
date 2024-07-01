using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI component that displays the game playing timer.
/// </summary>
public class GamePlayingClockUI : MonoBehaviour
{
    [Tooltip("Image component used to display the timer.")]
    [SerializeField] private Image timerImage;

    [Tooltip("Reference to the GameManager managing game time.")]
    [SerializeField] KitchenGameManager gameManager;

    private void Update()
    {
        timerImage.fillAmount = gameManager.GetPlayingTimerNormalized();
    }
}
