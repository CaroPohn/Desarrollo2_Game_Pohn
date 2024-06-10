using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float sensitivity;
    private float angle;
    private Transform mainCamera;

    private void Awake()
    {
        if (!target)
            Debug.LogError("There is no target assigned!");

        mainCamera = Camera.main.transform;
        mainCamera.localPosition = offset;
    }

    private void LateUpdate()
    {
        transform.position = target.position;
        RotateCamera();
    }

    void RotateCamera()
    {
        transform.Rotate(transform.up, angle * sensitivity * Time.deltaTime);
    }

    public void SetRotationAngle(Vector2 mouseDelta)
    {
        this.angle = mouseDelta.normalized.x;
    }
}
