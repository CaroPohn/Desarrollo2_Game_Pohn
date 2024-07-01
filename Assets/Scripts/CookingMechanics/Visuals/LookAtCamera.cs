using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Forces the object to constantly look at the main camera in the scene.
/// </summary>
public class LookAtCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
