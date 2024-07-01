using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject defining properties of a kitchen object.
/// </summary>
[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    [Header("Gameplay")]
    [Tooltip("Name of the kitchen object.")]
    [SerializeField] public string objectName;

    [Tooltip("Sprite representing the kitchen object.")]
    [SerializeField] public Sprite sprite;

    [Header("Config")]
    [Tooltip("Mesh used for rendering the kitchen object.")]
    [SerializeField] public Mesh mesh;

    [Tooltip("Material applied to the kitchen object.")]
    [SerializeField] public Material material;
}
