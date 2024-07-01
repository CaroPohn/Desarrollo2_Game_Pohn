using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Running : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 dir = Vector3.zero;

    [SerializeField] private Transform mainCameraPivot;

    [SerializeField] private float movementForce;
    [SerializeField] private float counterMovementForce;
    private Vector3 counterMovement;

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

    public void SetDir(Vector3 newDir)
    {
        dir = newDir;
    }

    private void ReturnToSpawn_OnRespawning()
    {
        isRespawning = true;
        animator.SetBool("isRunning", false);
    }

    private void ReturnToSpawn_OnRespawned()
    {
        isRespawning = false;   
    }
}
