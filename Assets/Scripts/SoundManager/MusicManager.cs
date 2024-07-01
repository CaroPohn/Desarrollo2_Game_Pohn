using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the music volume based on slider changes.
/// </summary>
public class MusicManager : MonoBehaviour
{
    AudioSource musicSource;
    private float musicVolume;

    private void Awake()
    {
        SliderManager.OnMusicVolumeChange += UpdateMusicVolume;
    }

    private void OnDestroy()
    {
        SliderManager.OnMusicVolumeChange -= UpdateMusicVolume;
    }

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();

        musicSource.volume = musicVolume;
    }

    /// <summary>
    /// Updates the music volume and applies it to the music source.
    /// </summary>
    /// <param name="newMusicVolume">The new music volume to apply.</param>
    private void UpdateMusicVolume(float newMusicVolume)
    {
        musicVolume = newMusicVolume;

        musicSource.volume = musicVolume;
    }
}
