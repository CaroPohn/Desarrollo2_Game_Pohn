using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Running running;
    public CameraLook cameraLook;

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (running != null)
            running.SetDir(moveDirection);
    }

    public void HandleCameraLook(InputAction.CallbackContext context)
    {
        cameraLook.SetRotationAngle(context.ReadValue<Vector2>());
    }
}
