using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Main menu UI control and functionality.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Canvas")]
    [Tooltip("The main menu canvas GameObject")]
    [SerializeField] private GameObject mainMenuCanvas;

    [Tooltip("Button to start playing the game")]
    [SerializeField] private Button playButton;

    [Tooltip("Button to view credits")]
    [SerializeField] private Button creditsButton;

    [Tooltip("Button to view controls")]
    [SerializeField] private Button controlsButton;

    [Tooltip("Button to exit the game")]
    [SerializeField] private Button exitButton;

    [Header("Credits Canvas")]
    [Tooltip("The credits canvas GameObject")]
    [SerializeField] private GameObject creditsCanvas;

    [Tooltip("Button to return to main menu from credits")]
    [SerializeField] private Button creditsBackButton;

    [Header("Controls Canvas")]
    [Tooltip("The controls canvas GameObject")]
    [SerializeField] private GameObject controlsCanvas;

    [Tooltip("Button to return to main menu from controls")]
    [SerializeField] private Button controlsBackButton;

    [Tooltip("Slider for sensitivity adjustment")]
    [SerializeField] private Slider sensitivityBar;

    [Tooltip("Slider for sound effects volume adjustment")]
    [SerializeField] private Slider soundBar;

    [Tooltip("Slider for music volume adjustment")]
    [SerializeField] private Slider musicBar;

    private GameObject currentCanvas;

    private void Awake()
    {
        playButton.onClick.AddListener(StartPlayScene);
        creditsButton.onClick.AddListener(GoToCreditsCanvas);
        controlsButton.onClick.AddListener(GoToControlsCanvas);
        exitButton.onClick.AddListener(QuitGame);

        creditsBackButton.onClick.AddListener(GoToMenuCanvas);
        controlsBackButton.onClick.AddListener(GoToMenuCanvas);

        if (PlayerPrefs.HasKey("sensitivity"))
        {
            sensitivityBar.value = PlayerPrefs.GetInt("sensitivity");
        }
        else
        {
            sensitivityBar.value = sensitivityBar.maxValue / 2;
            PlayerPrefs.SetInt("sensitivity", (int)sensitivityBar.value);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("volume"))
        {
            soundBar.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            soundBar.value = soundBar.maxValue / 2;
            PlayerPrefs.SetFloat("volume", soundBar.value);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicBar.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            musicBar.value = musicBar.maxValue / 2;
            PlayerPrefs.SetFloat("musicVolume", musicBar.value);
            PlayerPrefs.Save();
        }

        sensitivityBar.onValueChanged.AddListener(UpdateSensitivity);
        soundBar.onValueChanged.AddListener(UpdateVolume);
        musicBar.onValueChanged.AddListener(UpdateMusicVolume);

        Time.timeScale = 1f;
    }

    private void Start()
    {
        ChangeCurrentCanvas(mainMenuCanvas, playButton.gameObject);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(StartPlayScene);
        creditsButton.onClick.RemoveListener(GoToCreditsCanvas);
        controlsButton.onClick.RemoveListener(GoToControlsCanvas);
        exitButton.onClick.RemoveListener(QuitGame);

        creditsBackButton.onClick.RemoveListener(GoToMenuCanvas);
        controlsBackButton.onClick.RemoveListener(GoToMenuCanvas);

        sensitivityBar.onValueChanged.RemoveListener(UpdateSensitivity);
        soundBar.onValueChanged.RemoveListener(UpdateVolume);
        musicBar.onValueChanged.RemoveListener(UpdateMusicVolume);
    }

    /// <summary>
    /// Updates sensitivity value and saves it to PlayerPrefs.
    /// </summary>
    private void UpdateSensitivity(float sensitivity)
    {
        PlayerPrefs.SetInt("sensitivity", (int)sensitivity);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Updates sound effects volume value and saves it to PlayerPrefs.
    /// </summary>
    private void UpdateVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Updates music volume value and saves it to PlayerPrefs.
    /// </summary>
    private void UpdateMusicVolume(float musicVolume)
    {
        PlayerPrefs.SetFloat("volume", musicVolume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    private void StartPlayScene()
    {
        SceneLoader.Instance.ChangeScene("Tutorial");
    }

    /// <summary>
    /// Switches to the credits canvas.
    /// </summary>
    private void GoToCreditsCanvas()
    {
        ChangeCurrentCanvas(creditsCanvas, creditsBackButton.gameObject);
    }

    /// <summary>
    /// Switches to the controls canvas.
    /// </summary>
    private void GoToControlsCanvas()
    {
        ChangeCurrentCanvas(controlsCanvas, controlsBackButton.gameObject);
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    private void QuitGame()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
#endif
        Application.Quit();
    }

    /// <summary>
    /// Switches to the main menu canvas.
    /// </summary>
    private void GoToMenuCanvas()
    {
        ChangeCurrentCanvas(mainMenuCanvas, playButton.gameObject);
    }

    /// <summary>
    /// Activates the specified canvas and sets the selected game object for input.
    /// </summary>
    private void ChangeCurrentCanvas(GameObject newCanvas, GameObject selectedGameObject)
    {
        if(currentCanvas != null)
            currentCanvas.SetActive(false);

        newCanvas.SetActive(true);

        currentCanvas = newCanvas;
        EventSystem.current.SetSelectedGameObject(selectedGameObject);
    }
}
