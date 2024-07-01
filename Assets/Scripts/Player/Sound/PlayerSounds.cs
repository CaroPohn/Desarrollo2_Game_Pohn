using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player sound effects based on movement and jumping states.
/// </summary>
public class PlayerSounds : MonoBehaviour
{
    [Tooltip("Reference to the Running component of the player.")]
    [SerializeField] private Running playerRunning;

    [Tooltip("Reference to the Jumping component of the player.")]
    [SerializeField] private Jumping playerJumping;
    
    private float footstepTimer;
    private float footstepTimerMax = 0.2f;

    public static event EventHandler OnPlayerStep;

    private void Update()
    {
        footstepTimer -= Time.deltaTime;

        if(footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;

            if(playerRunning.isRunning && !playerJumping.jumping)
                OnPlayerStep.Invoke(this, EventArgs.Empty);
        }
    }
}
