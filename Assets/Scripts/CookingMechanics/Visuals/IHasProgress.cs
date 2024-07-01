using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for objects that have progress and can notify when progress changes.
/// </summary>
public interface IHasProgress 
{
    /// <summary>
    /// Event triggered when the progress of the object changes.
    /// </summary>
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    /// <summary>
    /// Event arguments for the progress changed event, containing normalized progress value.
    /// </summary>
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
