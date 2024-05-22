using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        Interact.Instance.OnSelectedCounterChanged += Interact_OnSelectedCounterChanged;
    }

    private void Interact_OnSelectedCounterChanged(object sender, Interact.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == clearCounter)
        {
            visualGameObject.SetActive(true);
        }
        else
        {
            visualGameObject.SetActive(false);
        }
    }
}
