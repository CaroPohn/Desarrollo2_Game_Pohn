using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Running running;
    public Jumping jumping;
    public CameraLook cameraLook;
    public Interact interact;
    public KitchenGameManager gameManager;
    public FlyMode flyMode;

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
        
        if(flyMode && context.started)
            flyMode.Fly();
    }

    public void HandleInteractInput(InputAction.CallbackContext context)
    {
        if (interact && context.started)
            interact.HandleInteraction();
    }

    public void HandleInteractAlternateInput(InputAction.CallbackContext context)
    {
        if (interact && context.started)
            interact.HandleInteractionAlternate();
    }

    public void HandlePause(InputAction.CallbackContext context)
    {
        if (gameManager && context.started)
            gameManager.TogglePauseGame();
    }

    public void HandleNextLevel(InputAction.CallbackContext context)
    {
        if (gameManager && context.started)
            gameManager.GoToNextLevel();
    }

    public void HandleGodMode(InputAction.CallbackContext context)
    {
        if (flyMode && context.started)
            flyMode.ToggleFlyMode();
    }

    public void HandleFlash(InputAction.CallbackContext context)
    {
        if (gameManager && context.started)
            gameManager.ToggleFlashMode();
    }
}
