using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    [Tooltip("Audio clips for chopping")]
    public AudioClip[] chop;

    [Tooltip("Audio clips for failed delivery")]
    public AudioClip[] deliveryFail;

    [Tooltip("Audio clips for successful delivery")]
    public AudioClip[] deliverySuccess;

    [Tooltip("Audio clips for footsteps")]
    public AudioClip[] footstep;

    [Tooltip("Audio clips for object dropping")]
    public AudioClip[] objectDrop;

    [Tooltip("Audio clips for object pickup")]
    public AudioClip[] objectPickup;

    [Tooltip("Audio clip for stove sizzling")]
    public AudioClip stoveSizzle;

    [Tooltip("Audio clips for trash disposal")]
    public AudioClip[] trash;

    [Tooltip("Audio clips for warning sounds")]
    public AudioClip[] warning;
}
