using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Canvas")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button exitButton;

    [Header("Credits Canvas")]
    [SerializeField] private GameObject creditsCanvas;
    [SerializeField] private Button creditsBackButton;

    [Header("Controls Canvas")]
    [SerializeField] private GameObject controlsCanvas;
    [SerializeField] private Button controlsBackButton;
    [SerializeField] private Slider sensitivityBar;
    [SerializeField] private Slider soundBar;
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

        sensitivityBar.onValueChanged.AddListener(UpdateSensitivity);
        soundBar.onValueChanged.AddListener(UpdateVolume);
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
    }
    private void UpdateSensitivity(float sensitivity)
    {
        PlayerPrefs.SetInt("sensitivity", (int)sensitivity);
        PlayerPrefs.Save();
    }

    private void UpdateVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    private void StartPlayScene()
    {
        SceneLoader.Instance.ChangeScene("Tutorial");
    }

    private void GoToCreditsCanvas()
    {
        ChangeCurrentCanvas(creditsCanvas, creditsBackButton.gameObject);
    }

    private void GoToControlsCanvas()
    {
        ChangeCurrentCanvas(controlsCanvas, controlsBackButton.gameObject);
    }

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

    private void GoToMenuCanvas()
    {
        ChangeCurrentCanvas(mainMenuCanvas, playButton.gameObject);
    }

    private void ChangeCurrentCanvas(GameObject newCanvas, GameObject selectedGameObject)
    {
        if(currentCanvas != null)
            currentCanvas.SetActive(false);

        newCanvas.SetActive(true);

        currentCanvas = newCanvas;
        EventSystem.current.SetSelectedGameObject(selectedGameObject);
    }
}
