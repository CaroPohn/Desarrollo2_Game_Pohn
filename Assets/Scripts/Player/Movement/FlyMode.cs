using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMode : MonoBehaviour
{
    private bool isFlying;
    private bool wantsToFly;
    private Rigidbody rb;

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

            Debug.Log("adentro del flying update");
        }
        else
        {
            rb.useGravity = true;
        }
    }

    public void ToggleFlyMode()
    {
        isFlying = !isFlying;
        jumping.enabled = !isFlying;
        Debug.Log("toggle fly mode");
        Debug.Log("is flying " + isFlying);
        Debug.Log("jumping state " + jumping.enabled);
    }

    public void Fly()
    {
        if (isFlying)
        {
            wantsToFly = !wantsToFly;
            Debug.Log("entro al Fly If");
            Debug.Log("wants to fly" + wantsToFly);
        }
        else
            Debug.Log("no entro al fly if");
    }
}
