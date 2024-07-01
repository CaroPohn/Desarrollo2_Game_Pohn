using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles sound effects for the stove counter in the kitchen.
/// </summary>
public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
