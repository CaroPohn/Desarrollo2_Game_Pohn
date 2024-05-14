using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private int menuSceneIndex = 0;

    public void BackMenu()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }
}
