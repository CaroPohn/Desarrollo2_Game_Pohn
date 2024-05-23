using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public enum scenes
    {
        MainMenu,
        Credits,
        Controls,
        InitialLevel,
    }

    public void Play()
    {
        SceneManager.LoadScene((int)scenes.InitialLevel);
    }

    public void Credits()
    {
        SceneManager.LoadScene((int)scenes.Credits);
    }

    public void Controls()
    {
        SceneManager.LoadScene((int)scenes.Controls);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
#endif
        Application.Quit();
    }
}
