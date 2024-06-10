using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public virtual void Enter()
    {
        Debug.Log("Enter");
    }
    public virtual void Update()
    {
        Debug.Log("Update");
    }
    public virtual void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }
    public virtual void LateUpdate()
    {
        Debug.Log("LateUpdate");
    }
    public virtual void Exit()
    {
        Debug.Log("Exit");
    }
}
