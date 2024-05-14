using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Running : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float speed = 0;
    private Vector3 dir = Vector3.zero;

    [SerializeField] private Transform mainCameraPivot;

    private float brakeMultiplier = .15f;
    private bool shouldBrake;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();

        var currentVelocity = rb.velocity;
        currentVelocity.y = 0;

        if (shouldBrake)
        {
            rb.AddForce(-currentVelocity * brakeMultiplier, ForceMode.Impulse);
            shouldBrake = false;
        }
    }

    public void Move()
    {
        if (dir.magnitude < 0.0001f)
        {
            shouldBrake = true;
        }

        Vector3 movementDir = mainCameraPivot.right * dir.x + mainCameraPivot.forward * dir.z;

        transform.forward = Vector3.Lerp(transform.forward, movementDir, 0.4f);

        rb.AddForce(movementDir * speed, ForceMode.Force);
    }

    public void SetDir(Vector3 newDir)
    {
        dir = newDir;
    }
}
