using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Interact : MonoBehaviour
{
    public static Interact Instance { get; private set; }
    
    private ClearCounter selectedCounter;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one Interact instance");
        }
        Instance = this;
    }

    private void Update()
    {
        SelectCounter();
    }

    private void SelectCounter()
    {
        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);

                    OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
                    {
                        selectedCounter = selectedCounter
                    });
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }

        Debug.Log(selectedCounter);
    }

    public void HandleInteractions()
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
