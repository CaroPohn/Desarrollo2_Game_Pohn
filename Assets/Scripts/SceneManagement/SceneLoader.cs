using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton class to manage scene loading and unloading.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;

    /// <summary>
    /// Instance of the SceneLoader singleton.
    /// </summary>
    public static SceneLoader Instance => instance;

    [Tooltip("Name of the scene to load at startup.")]
    [SerializeField] private string startingScene;

    private string currentScene;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        ChangeScene(startingScene);
    }

    /// <summary>
    /// Changes the active scene to the specified scene name.
    /// </summary>
    /// <param name="sceneName">Name of the scene to load.</param>
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(ChangingScene(sceneName));
    }

    /// <summary>
    /// Coroutine that asynchronously unloads the current scene and loads a new scene additively.
    /// </summary>
    /// <param name="sceneName">Name of the scene to load.</param>
    private IEnumerator ChangingScene(string sceneName)
    {
        if (currentScene != null)
        {
            var unloadOperation = SceneManager.UnloadSceneAsync(currentScene);

            while(!unloadOperation.isDone) 
            {
                yield return null;
            }
        }

        var loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            yield return null;
        }


        currentScene = sceneName;
    }
}

