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

    private GameObject currentCanvas;

    private void Awake()
    {
        playButton.onClick.AddListener(StartPlayScene);
        creditsButton.onClick.AddListener(GoToCreditsCanvas);
        controlsButton.onClick.AddListener(GoToControlsCanvas);
        exitButton.onClick.AddListener(QuitGame);

        creditsBackButton.onClick.AddListener(GoToMenuCanvas);
        controlsBackButton.onClick.AddListener(GoToMenuCanvas);
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
