using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SerializableScene : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField] private UnityEditor.SceneAsset scene;

#endif

    [field: SerializeField] public int BuildIndex { get; set; }

    public void Rasterize()
    {
#if UNITY_EDITOR

        if (scene)
        {
            BuildIndex = SceneUtility.GetBuildIndexByScenePath(UnityEditor.AssetDatabase.GetAssetPath(scene));
        }

#endif
    }
}
