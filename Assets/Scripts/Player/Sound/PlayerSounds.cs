using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private Running playerRunning;
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
