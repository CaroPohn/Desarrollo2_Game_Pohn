using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Running running;
    public Jumping jumping;
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

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (jumping && context.started)
            jumping.StartJump();
    }
}
