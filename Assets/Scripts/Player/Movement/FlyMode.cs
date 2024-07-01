using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the player to toggle and control flying mode.
/// </summary>
public class FlyMode : MonoBehaviour
{
    private bool isFlying;
    private bool wantsToFly;
    private Rigidbody rb;

    [Tooltip("Vertical velocity while flying.")]
    [SerializeField] private float velocity;

    private Jumping jumping;

    private void Start()
    {
        jumping = GetComponent<Jumping>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isFlying && wantsToFly)
        {
            rb.useGravity = false;

            Vector3 playerPosition = transform.position;

            playerPosition.y += velocity * Time.deltaTime;

            transform.position = playerPosition;
        }
        else
        {
            rb.useGravity = true;
        }
    }

    /// <summary>
    /// Toggles the flying mode on and off.
    /// </summary>
    public void ToggleFlyMode()
    {
        isFlying = !isFlying;
        jumping.enabled = !isFlying;
    }

    /// <summary>
    /// Initiates or cancels the desire to fly based on current flying state.
    /// </summary>
    public void Fly()
    {
        if (isFlying)
        {
            wantsToFly = !wantsToFly;
        }
    }
}
