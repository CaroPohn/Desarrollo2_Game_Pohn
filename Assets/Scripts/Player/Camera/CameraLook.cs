using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Tooltip("The target transform the camera should follow.")]
    [SerializeField] private Transform target;

    [Tooltip("The offset from the target's position.")]
    [SerializeField] private Vector3 offset;

    private int sensitivity;
    private float angle;
    private Transform mainCamera;

    private void Awake()
    {
        if (!target)
            Debug.LogError("There is no target assigned!");

        mainCamera = Camera.main.transform;
        mainCamera.localPosition = offset;

        SliderManager.OnSensitivityChange += UpdateSensitivity;
    }

    private void OnDestroy()
    {
        SliderManager.OnSensitivityChange -= UpdateSensitivity;
    }

    private void LateUpdate()
    {
        transform.position = target.position;

        RotateCamera();
    }

    /// <summary>
    /// Updates the camera sensitivity when the global sensitivity setting changes.
    /// </summary>
    /// <param name="newSensitivity">New sensitivity value.</param>
    private void UpdateSensitivity(int newSensitivity)
    {
        sensitivity = newSensitivity;
    }

    /// <summary>
    /// Rotates the camera horizontally based on the current angle and sensitivity.
    /// </summary>
    void RotateCamera()
    {
        transform.Rotate(transform.up, angle * sensitivity * Time.deltaTime);
    }

    /// <summary>
    /// Sets the rotation angle of the camera based on mouse movement.
    /// </summary>
    /// <param name="mouseDelta">Normalized mouse movement delta.</param>
    public void SetRotationAngle(Vector2 mouseDelta)
    {
        this.angle = mouseDelta.normalized.x;
    }
}
