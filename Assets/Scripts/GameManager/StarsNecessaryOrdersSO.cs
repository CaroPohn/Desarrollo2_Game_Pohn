using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject that stores level duration and required orders for stars.
/// </summary>
[CreateAssetMenu()]
public class StarsNecessaryOrdersSO : ScriptableObject
{
    [Header("Level duration")]
    [Tooltip("Maximum duration of the level in seconds.")]
    public float levelDuration;

    [Header("Stars required")]
    [Tooltip("Minimum orders required for the first star.")]
    public int firstStarOrders;

    [Tooltip("Minimum orders required for the second star.")]
    public int secondStarOrders;

    [Tooltip("Minimum orders required for the third star.")]
    public int thirdStarOrders;
}
