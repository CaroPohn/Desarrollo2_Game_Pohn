using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    bool isActive = false;

    private void Start()
    {

    }
    public void Interact_OnSelectedCounterChanged()
    {
        isActive = !isActive;
        visualGameObject.SetActive(isActive);
    }
}
