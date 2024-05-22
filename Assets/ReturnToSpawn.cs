using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToSpawn : MonoBehaviour
{
    [SerializeField] private Transform spawnpoint;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = spawnpoint.position;
    }
}
