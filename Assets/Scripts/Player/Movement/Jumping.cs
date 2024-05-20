using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private Transform feetPivot;
    [SerializeField] private float groundedDistance = 0.3f;

    [SerializeField] private LayerMask floor;

    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private float maxFloorAngle = 60f;

    [SerializeField] private float timeBetweenJump;

    private const float jumpAnimTime = 0.40f;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public IEnumerator JumpCoroutine()
    {
        if (!CanJump())
            yield break;
        animator.SetTrigger("jump");
        yield return new WaitForSeconds(jumpAnimTime);
        yield return new WaitForFixedUpdate();

        rb.AddForce(transform.up * jumpForce);
    }

    private bool CanJump()
    {
        if (Physics.Raycast(feetPivot.position, Vector3.down, out var hit, groundedDistance, floor))
        {
            var contactAngle = Vector3.Angle(hit.normal, Vector3.up);

            if (contactAngle >= maxFloorAngle)
                return false;
            return true;
        }

        return false;
    }
}
