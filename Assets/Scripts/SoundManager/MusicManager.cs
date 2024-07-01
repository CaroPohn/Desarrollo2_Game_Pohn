using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void UpdateMusicVolume(float newMusicVolume)
    {
        musicVolume = newMusicVolume;

        musicSource.volume = musicVolume;
    }
}
