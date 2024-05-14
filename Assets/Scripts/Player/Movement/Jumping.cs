using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float force = 0f;
    [SerializeField] private float groundedDistance = .001f;
    [SerializeField] private LayerMask floor;
    [SerializeField] private Transform feetPivot;
    [SerializeField] private float maxFloorAngle = 60f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public IEnumerator JumpCoroutine()
    {
        if (!CanJump())
            yield break;
        yield return new WaitForFixedUpdate();
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
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
