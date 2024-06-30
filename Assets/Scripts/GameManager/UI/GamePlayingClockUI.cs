using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    [SerializeField] KitchenGameManager gameManager;

    private void Update()
    {
        timerImage.fillAmount = gameManager.GetPlayingTimerNormalized();
    }
}