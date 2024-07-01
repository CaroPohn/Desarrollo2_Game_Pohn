using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Visual representation and animation control for the cutting counter.
/// </summary>
public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";

    [Tooltip("Reference to the cutting counter logic script.")]
    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    /// <summary>
    /// Triggered when the cutting counter performs a cut action.
    /// </summary>
    private void CuttingCounter_OnCut()
    {
        animator.SetTrigger(CUT);
    }
}
