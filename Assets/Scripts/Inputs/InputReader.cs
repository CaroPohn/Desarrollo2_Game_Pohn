using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Reads player input and delegates actions to corresponding game components.
/// </summary>
public class InputReader : MonoBehaviour
{
    [Tooltip("Component for controlling running behavior")]
    public Running running;

    [Tooltip("Component for controlling jumping behavior")]
    public Jumping jumping;

    [Tooltip("Component for controlling camera rotation")]
    public CameraLook cameraLook;

    [Tooltip("Component for handling interactions")]
    public Interact interact;

    [Tooltip("Game manager component for game state control")]
    public KitchenGameManager gameManager;

    [Tooltip("Component for controlling fly mode behavior")]
    public FlyMode flyMode;

    /// <summary>
    /// Handles movement input.
    /// </summary>
    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (running != null)
            running.SetDir(moveDirection);
    }

    /// <summary>
    /// Handles camera look input.
    /// </summary>
    public void HandleCameraLook(InputAction.CallbackContext context)
    {
        cameraLook.SetRotationAngle(context.ReadValue<Vector2>());
    }

    /// <summary>
    /// Handles jump and flying input.
    /// </summary>
    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (jumping && context.started)
            jumping.StartJump();
        
        if(flyMode && context.started)
            flyMode.Fly();
    }

    /// <summary>
    /// Handles primary interact input.
    /// </summary>
    public void HandleInteractInput(InputAction.CallbackContext context)
    {
        if (interact && context.started)
            interact.HandleInteraction();
    }

    /// <summary>
    /// Handles alternate interact input.
    /// </summary>
    public void HandleInteractAlternateInput(InputAction.CallbackContext context)
    {
        if (interact && context.started)
            interact.HandleInteractionAlternate();
    }

    /// <summary>
    /// Handles pause input.
    /// </summary>
    public void HandlePause(InputAction.CallbackContext context)
    {
        if (gameManager && context.started)
            gameManager.TogglePauseGame();
    }

    /// <summary>
    /// Handles next level input.
    /// </summary>
    public void HandleNextLevel(InputAction.CallbackContext context)
    {
        if (gameManager && context.started)
            gameManager.GoToNextLevel();
    }

    /// <summary>
    /// Handles god mode input, enabling fly mode.
    /// </summary>
    public void HandleGodMode(InputAction.CallbackContext context)
    {
        if (flyMode && context.started)
            flyMode.ToggleFlyMode();
    }

    /// <summary>
    /// Handles flash mode input.
    /// </summary>
    public void HandleFlash(InputAction.CallbackContext context)
    {
        if (gameManager && context.started)
            gameManager.ToggleFlashMode();
    }
}
