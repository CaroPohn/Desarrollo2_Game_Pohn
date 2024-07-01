using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// UI component that displays a countdown for respawn.
/// </summary>
public class RespawnCountdownUI : MonoBehaviour
{
    [Tooltip("Text component used to display the countdown.")]
    [SerializeField] private TextMeshProUGUI countdownText;

    private float countdownToSpawn = 5f;

    private void Awake()
    {
        ReturnToSpawn.OnRespawning += ReturnToSpawn_OnRespawning;
    }

    private void OnDestroy()
    {
        ReturnToSpawn.OnRespawning -= ReturnToSpawn_OnRespawning;
    }

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        countdownToSpawn -= Time.deltaTime;

        countdownText.text = Mathf.Ceil(countdownToSpawn).ToString();

        if (countdownToSpawn < 0f)
        {
            Hide();
            countdownToSpawn = 5f;
        }
    }

    /// <summary>
    /// Hides the countdown UI.
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Shows the countdown UI and resets the countdown timer.
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Event handler called when respawning is initiated, shows the countdown UI and resets the timer.
    /// </summary>
    private void ReturnToSpawn_OnRespawning()
    {
        Show();
        countdownToSpawn = 5f;
    }
}
