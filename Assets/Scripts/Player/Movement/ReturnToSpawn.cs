using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles respawning the object to a specified spawn point when it falls below a certain Y position.
/// </summar
public class ReturnToSpawn : MonoBehaviour
{
    [Tooltip("The transform representing the spawn point.")]
    [SerializeField] private Transform spawnpoint;

    /// <summary>
    /// Event triggered when respawning is initiated.
    /// </summary>
    public static event Action OnRespawning;

    /// <summary>
    /// Event triggered when respawning is completed.
    /// </summary>
    public static event Action OnRespawned;

    private bool isRespawning = false;

    private void Update()
    {
        if(transform.position.y < 0f && !isRespawning)
        {
            StartCoroutine(Respawn());
        }
    }

    /// <summary>
    /// Coroutine to handle the respawn process with a timer penalization.
    /// </summary>
    private IEnumerator Respawn()
    {
        OnRespawning?.Invoke();

        isRespawning = true;

        transform.position = spawnpoint.position;

        yield return new WaitForSeconds(5);

        isRespawning = false;

        OnRespawned?.Invoke();
    }
}
