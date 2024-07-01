using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the game flow and UI interactions for the kitchen game.
/// </summary>
public class KitchenGameManager : MonoBehaviour
{
    public event Action OnStateChanged;

    [Tooltip("Next scene to load after completing the current level.")]
    [SerializeField] private string nextScene;

    [Tooltip("Current level identifier.")]
    [SerializeField] private string currentLevel;

    [Tooltip("Button to proceed to the next level.")]
    [SerializeField] private Button nextLevelButton;

    [Tooltip("Button to play the current level again.")]
    [SerializeField] private Button playAgainButton;

    [Tooltip("Button to return to the main menu.")]
    [SerializeField] private Button backMenuButton;

    [Tooltip("Button to start the tutorial.")]
    [SerializeField] private Button playTutorialButton;

    [Tooltip("Panel containing the tutorial content.")]
    [SerializeField] private GameObject tutorial;

    [Tooltip("ScriptableObject containing necessary orders for winning stars.")]
    [SerializeField] private StarsNecessaryOrdersSO starsNecessary;

    static public event Action OnGamePaused;
    static public event Action OnGameUnpaused;

    private enum State
    {
        TutorialPanel,
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
        tutorial.gameObject.SetActive(false);

        if(currentLevel == "Tutorial")
        {
            state = State.TutorialPanel;
        }
        else
            state = State.WaitingToStart;

        playTutorialButton.onClick.AddListener(StartGameFromTutorial);
        nextLevelButton.onClick.AddListener(GoToNextLevel);
        playAgainButton.onClick.AddListener(PlayAgain);
        backMenuButton.onClick.AddListener(GoToMainMenu);
    }

    private void OnDestroy()
    {
        playTutorialButton.onClick.RemoveListener(StartGameFromTutorial);
        nextLevelButton.onClick.RemoveListener(GoToNextLevel);
        playAgainButton.onClick.RemoveListener(PlayAgain);
        backMenuButton.onClick.RemoveListener(GoToMainMenu);
    }

    private void Update()
    {
        switch (state)
        {
            case State.TutorialPanel:
                tutorial.gameObject.SetActive(true);
                break;

            case State.WaitingToStart:
                tutorial.gameObject.SetActive(false);
                waitingToStartTimer -= Time.deltaTime;
                Time.timeScale = 1f;

                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;

                    waitingToStartTimer = 1f;
                    gamePlayingTimerMax = starsNecessary.levelDuration;

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

    /// <summary>
    /// Checks if the game is currently in the playing state.
    /// </summary>
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    /// <summary>
    /// Checks if the countdown to start state is active.
    /// </summary>
    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    /// <summary>
    /// Retrieves the current countdown to start timer value.
    /// </summary>
    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    /// <summary>
    /// Checks if the game is currently in the game over state.
    /// </summary>
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    /// <summary>
    /// Retrieves the normalized value of the current playing timer.
    /// </summary>
    public float GetPlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    /// <summary>
    /// Loads the next level scene.
    /// </summary>
    public void GoToNextLevel()
    {
        SceneLoader.Instance.ChangeScene(nextScene);
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    private void GoToMainMenu()
    {
        SceneLoader.Instance.ChangeScene("Main_Menu");
    }

    /// <summary>
    /// Restarts the current level scene.
    /// </summary>
    private void PlayAgain()
    {
        SceneLoader.Instance.ChangeScene(currentLevel);
    }

    /// <summary>
    /// Toggles the pause state of the game.
    /// </summary>
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

    /// <summary>
    /// Toggles the flash mode state of the game.
    /// </summary>
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

    /// <summary>
    /// Starts the game from the tutorial state to waiting to start state.
    /// </summary>
    public void StartGameFromTutorial()
    {
        state = State.WaitingToStart;
    }
}
