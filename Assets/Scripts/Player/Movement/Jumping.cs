using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles jumping, double jumping and coyote time mechanics for the player.
/// </summary>
public class Jumping : MonoBehaviour
{
    private Rigidbody rb;

    public bool jumping;
    private bool canDoubleJump = true;

    [Tooltip("Transform representing the feet position of the player.")]
    [SerializeField] private Transform feetPivot;

    [Tooltip("Distance to detect the floor for grounding.")]
    [SerializeField] private float floorDistance = 0.3f;

    [Tooltip("Layer mask to detect floor objects.")]
    [SerializeField] private LayerMask floorLayer;

    [Tooltip("Force applied when jumping.")]
    [SerializeField] private float jumpForce = 0f;

    [Tooltip("Time interval between jumps.")]
    [SerializeField] private float timeBetweenJump = 0.2f;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [Tooltip("Animator that contains player animations.")]
    [SerializeField] private Animator animator;
    Coroutine jumpCoroutine;

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
        if (CanJump())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Initiates a jump action, checking for conditions like coyote time and double jump availability.
    /// </summary>
    public void StartJump()
    {
        if (((coyoteTimeCounter > 0f && !jumping) || canDoubleJump) && !isRespawning)
        {
            if (jumpCoroutine != null)
                StopCoroutine(jumpCoroutine);

            jumpCoroutine = StartCoroutine(JumpCoroutine());
        }
    }


    private IEnumerator JumpCoroutine()
    {
        if (jumping)
        {
            canDoubleJump = false;
            animator.SetBool("doubleJump", true);
            if (rb.velocity.y < 0)
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
        else
            animator.SetBool("isJumping", true);

        jumping = true;

        yield return new WaitForFixedUpdate();

        rb.AddForce(transform.up * jumpForce);

        if (!CanJump() && !canDoubleJump)
            yield return new WaitForSeconds(timeBetweenJump);

        animator.SetBool("isJumping", false);
        animator.SetBool("doubleJump", false);
    }

    /// <summary>
    /// Checks if the player is currently able to jump based on ground detection and jumping state.
    /// </summary>
    public bool CanJump()
    {
        return Physics.CheckSphere(feetPivot.position, floorDistance, floorLayer) && !jumping;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(feetPivot.position, floorDistance);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            animator.SetBool("isFalling", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            animator.SetBool("isFalling", false);
            jumping = false;
            canDoubleJump = true;
        }
    }

    /// <summary>
    /// Event handler for when the player character is respawning.
    /// </summary>
    private void ReturnToSpawn_OnRespawned()
    {
        isRespawning = false;
    }

    /// <summary>
    /// Event handler for when the player character has finished respawning.
    /// </summary>
    private void ReturnToSpawn_OnRespawning()
    {
        isRespawning = true;
    }
}
