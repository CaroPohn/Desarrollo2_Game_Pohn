using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider sensitivityBar;
    [SerializeField] private Slider sfxVolumeBar;
    [SerializeField] private Slider musicVolumeBar;

    static public Action<int> OnSensitivityChange;
    static public Action<float> OnSFXVolumeChange;
    static public Action<float> OnMusicVolumeChange;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("sensitivity"))
        {
            sensitivityBar.value = PlayerPrefs.GetInt("sensitivity");
        }
        else
        {
            sensitivityBar.value = sensitivityBar.maxValue / 2;
            PlayerPrefs.SetInt("sensitivity", (int)sensitivityBar.value);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("volume"))
        {
            sfxVolumeBar.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            sfxVolumeBar.value = sfxVolumeBar.maxValue / 2;
            PlayerPrefs.SetFloat("volume", sfxVolumeBar.value);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolumeBar.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            musicVolumeBar.value = musicVolumeBar.maxValue / 2;
            PlayerPrefs.SetFloat("musicVolume", musicVolumeBar.value);
            PlayerPrefs.Save();
        }

        sensitivityBar.onValueChanged.AddListener(UpdateSensitivity);
        sfxVolumeBar.onValueChanged.AddListener(UpdateSFXVolume);
        musicVolumeBar.onValueChanged.AddListener(UpdateMusicVolume);
    }

    private void Start()
    {
        OnSensitivityChange?.Invoke((int)sensitivityBar.value);
        OnSFXVolumeChange?.Invoke(sfxVolumeBar.value);
        OnMusicVolumeChange?.Invoke(musicVolumeBar.value);
    }

    private void OnDestroy()
    {
        sensitivityBar.onValueChanged.RemoveListener(UpdateSensitivity);
        sfxVolumeBar.onValueChanged.RemoveListener(UpdateSFXVolume);
        musicVolumeBar.onValueChanged.RemoveListener(UpdateMusicVolume);
    }

    private void UpdateSensitivity(float sensitivity)
    {
        PlayerPrefs.SetInt("sensitivity", (int)sensitivity);
        PlayerPrefs.Save();

        OnSensitivityChange?.Invoke((int)sensitivity);
    }

    private void UpdateSFXVolume(float sfxVolume)
    {
        PlayerPrefs.SetFloat("volume", sfxVolume);
        PlayerPrefs.Save();

        OnSFXVolumeChange?.Invoke(sfxVolume);
    }

    private void UpdateMusicVolume(float musicVolume)
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.Save();

        OnMusicVolumeChange?.Invoke(musicVolume);
    }
}
