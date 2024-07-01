using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenGameManager : MonoBehaviour
{
    public event Action OnStateChanged;

    [SerializeField] private string nextScene;
    [SerializeField] private string currentLevel;

    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button backMenuButton;

    static public event Action OnGamePaused;
    static public event Action OnGameUnpaused;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;

    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 50f;

    private bool isGamePaused = false;

    private bool isFlashModeActive = false;

    private void Awake()
    {
        state = State.WaitingToStart;

        nextLevelButton.onClick.AddListener(GoToNextLevel);
        playAgainButton.onClick.AddListener(PlayAgain);
        backMenuButton.onClick.AddListener(GoToMainMenu);
    }

    private void OnDestroy()
    {
        nextLevelButton.onClick.RemoveListener(GoToNextLevel);
        playAgainButton.onClick.RemoveListener(PlayAgain);
        backMenuButton.onClick.RemoveListener(GoToMainMenu);
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
               
                waitingToStartTimer -= Time.deltaTime;
                Time.timeScale = 1f;

                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;

                    waitingToStartTimer = 1f;

                    OnStateChanged?.Invoke();
                }
                break; 


            case State.CountdownToStart:

                countdownToStartTimer -= Time.deltaTime;

                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;

                    gamePlayingTimer = gamePlayingTimerMax;
                    countdownToStartTimer = 3f;

                    OnStateChanged?.Invoke();
                }
                break;

            case State.GamePlaying:

                gamePlayingTimer -= Time.deltaTime;

                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke();
                }
                break;

            case State.GameOver:

                Time.timeScale = 0f;
                break;
        }

    }
    
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetPlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void GoToNextLevel()
    {
        SceneLoader.Instance.ChangeScene(nextScene);
    }

    private void GoToMainMenu()
    {
        SceneLoader.Instance.ChangeScene("Main_Menu");
    }

    private void PlayAgain()
    {
        SceneLoader.Instance.ChangeScene(currentLevel);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;

        if(isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke();
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke();
        }
    }

    public void ToggleFlashMode()
    {
        isFlashModeActive = !isFlashModeActive;

        if (isFlashModeActive)
        {
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
