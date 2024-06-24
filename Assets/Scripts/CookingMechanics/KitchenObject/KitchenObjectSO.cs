using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    [Header("Gameplay")]
    public Transform prefab;
    [SerializeField] public string objectName;
    [Header("Config")]
    [SerializeField] public Mesh mesh;
    [SerializeField] public Material material;
}
