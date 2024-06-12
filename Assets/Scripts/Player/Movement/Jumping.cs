using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    private Rigidbody rb;

    private bool jumping;
    private bool canDoubleJump = true;

    [SerializeField] private Transform feetPivot;
    [SerializeField] private float floorDistance = 0.3f;

    [SerializeField] private LayerMask floorLayer;

    [SerializeField] private float jumpForce = 0f;

    [SerializeField] private float timeBetweenJump = 0.2f;

    private float coyoteTime = 0.5f;
    private float coyoteTimeCounter;

    [SerializeField] private Animator animator;
    Coroutine jumpCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

    public void StartJump()
    {
        if ((coyoteTimeCounter > 0f && !jumping) || canDoubleJump)
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
}
