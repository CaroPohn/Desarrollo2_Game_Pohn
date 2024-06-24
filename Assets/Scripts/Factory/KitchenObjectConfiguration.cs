using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectConfiguration : ScriptableObject
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    [SerializeField] private KitchenObjectSO kitchenObjSO;
}
