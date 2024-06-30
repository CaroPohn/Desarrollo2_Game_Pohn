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
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
               
                waitingToStartTimer -= Time.deltaTime;

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

    private void GoToNextLevel()
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
}
