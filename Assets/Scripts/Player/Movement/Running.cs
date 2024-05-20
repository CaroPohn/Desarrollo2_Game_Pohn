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

    //private AnimationsController animationsController;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector3 movementDir = mainCameraPivot.right * dir.x + mainCameraPivot.forward * dir.z;
        counterMovement = new Vector3(-rb.velocity.x * counterMovementForce, 0, -rb.velocity.z * counterMovementForce);

        transform.forward = Vector3.Lerp(transform.forward, movementDir, 0.4f);

        rb.AddForce(movementDir.normalized * movementForce + counterMovement);

        //animationsController.playerAnimator.SetBool("isRunning", true);

        animator.SetBool("isRunning", true);

        if(dir ==  Vector3.zero)
        {
            //animationsController.playerAnimator.SetBool("isRunning", false);
            animator.SetBool("isRunning", false);
        }
    }

    public void SetDir(Vector3 newDir)
    {
        dir = newDir;
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("floor"))
        {
            animator.SetBool("isFalling", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            animator.SetBool("isFalling", false);
        }
    }
}
