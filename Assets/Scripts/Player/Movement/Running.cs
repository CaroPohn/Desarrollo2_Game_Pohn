using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Controls the movement of the player character based on input direction.
/// </summary>
public class Running : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 dir = Vector3.zero;

    [Tooltip("Transform representing the pivot point of the main camera.")]
    [SerializeField] private Transform mainCameraPivot;

    [Tooltip("Force applied to move the player.")]
    [SerializeField] private float movementForce;

    [Tooltip("Counter force applied to slow down the player's movement.")]
    [SerializeField] private float counterMovementForce;

    private Vector3 counterMovement;

    [Tooltip("Animator component responsible for controlling animations.")]
    [SerializeField] private Animator animator;

    public bool isRunning;

    private bool isRespawning;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        ReturnToSpawn.OnRespawning += ReturnToSpawn_OnRespawning;
        ReturnToSpawn.OnRespawned += ReturnToSpawn_OnRespawned;
    }


    private void OnDestroy()
    {
        ReturnToSpawn.OnRespawning -= ReturnToSpawn_OnRespawning;
        ReturnToSpawn.OnRespawned -= ReturnToSpawn_OnRespawned;
    }

    private void FixedUpdate()
    {
        if(!isRespawning)
        {
            Move();
        }
        else
            rb.AddForce(-new Vector3(rb.velocity.x, 0, rb.velocity.z), ForceMode.Impulse);
    }

    /// <summary>
    /// Moves the player character in the calculated direction.
    /// </summary>
    public void Move()
    {
        Vector3 movementDir = mainCameraPivot.right * dir.x + mainCameraPivot.forward * dir.z;
        counterMovement = new Vector3(-rb.velocity.x * counterMovementForce, 0, -rb.velocity.z * counterMovementForce);

        transform.forward = Vector3.Lerp(transform.forward, movementDir, 0.4f);

        rb.AddForce(movementDir.normalized * movementForce + counterMovement);

        animator.SetBool("isRunning", true);

        isRunning = true;

        if(dir ==  Vector3.zero)
        {
            animator.SetBool("isRunning", false);
            isRunning = false;
        }
    }

    /// <summary>
    /// Sets the movement direction input.
    /// </summary>
    /// <param name="newDir">New direction input vector.</param>
    public void SetDir(Vector3 newDir)
    {
        dir = newDir;
    }

    /// <summary>
    /// Handler for when the player is respawning.
    /// </summary>
    private void ReturnToSpawn_OnRespawning()
    {
        isRespawning = true;
        animator.SetBool("isRunning", false);
    }

    /// <summary>
    /// Handler for when the player has finished respawning.
    /// </summary>
    private void ReturnToSpawn_OnRespawned()
    {
        isRespawning = false;   
    }
}
