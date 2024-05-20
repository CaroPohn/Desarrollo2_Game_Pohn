using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    private Rigidbody rb;

    private bool jumping;
    private bool doubleJump;

    [SerializeField] private Transform feetPivot;
    [SerializeField] private float floorDistance = 0.3f;

    [SerializeField] private LayerMask floorLayer;

    [SerializeField] private float jumpForce = 0f;
    //[SerializeField] private float maxFloorAngle = 60f;

    [SerializeField] private float timeBetweenJump = 0.2f;

    private const float jumpAnimTime = 0.40f;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Debug.Log(doubleJump);

        if (CanJump() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }
    }

    public void StartJump()
    {
        if (CanJump() || doubleJump)
        {
            StartCoroutine(JumpCoroutine());
            doubleJump = !doubleJump;
        }
    }

    private IEnumerator JumpCoroutine()
    {
        jumping = true;
        animator.SetTrigger("jump");
        yield return new WaitForSeconds(jumpAnimTime);
        yield return new WaitForFixedUpdate();

        rb.AddForce(transform.up * jumpForce);

        yield return new WaitForSeconds(timeBetweenJump);

        jumping = false;
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
}
