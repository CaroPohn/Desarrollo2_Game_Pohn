using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToSpawn : MonoBehaviour
{
    [SerializeField] private Transform spawnpoint;

    static public event Action OnRespawning;
    static public event Action OnRespawned;

    private bool isRespawning = false;

    private void Update()
    {
        if(transform.position.y < 0f && !isRespawning)
        {
            StartCoroutine(Respawn());
        }
    }

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
