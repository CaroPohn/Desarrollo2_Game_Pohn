using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] DeliveryManager deliveryManager;
    [SerializeField] OrderCounter orderCounter;
    private float volumeConfig = 1f;

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            volumeConfig = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            PlayerPrefs.SetFloat("volume", volumeConfig);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        deliveryManager.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        deliveryManager.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Interact.OnPickedSomething += Interact_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        PlayerSounds.OnPlayerStep += PlayerSounds_OnPlayerStep;

        SliderManager.OnSFXVolumeChange += UpdateSFXVolume;
    }


    private void OnDestroy()
    {
        deliveryManager.OnRecipeSuccess -= DeliveryManager_OnRecipeSuccess;
        deliveryManager.OnRecipeFailed -= DeliveryManager_OnRecipeFailed;

        CuttingCounter.OnAnyCut -= CuttingCounter_OnAnyCut;
        Interact.OnPickedSomething -= Interact_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere -= BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed -= TrashCounter_OnAnyObjectTrashed;
        PlayerSounds.OnPlayerStep -= PlayerSounds_OnPlayerStep;

        SliderManager.OnSFXVolumeChange -= UpdateSFXVolume;
    }

    private void PlayerSounds_OnPlayerStep(object sender, System.EventArgs e)
    {
        PlayerSounds playerStep = sender as PlayerSounds;
        PlaySoundArray(audioClipRefsSO.footstep, playerStep.transform.position, volumeConfig);
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySoundArray(audioClipRefsSO.trash, trashCounter.transform.position, volumeConfig);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySoundArray(audioClipRefsSO.objectDrop, baseCounter.transform.position, volumeConfig);
    }

    private void Interact_OnPickedSomething(object sender, System.EventArgs e)
    {
        Interact playerInteract = sender as Interact;
        PlaySoundArray(audioClipRefsSO.objectPickup, playerInteract.transform.position, volumeConfig);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySoundArray(audioClipRefsSO.chop, cuttingCounter.transform.position, volumeConfig);
    }

    private void DeliveryManager_OnRecipeFailed()
    {
        PlaySoundArray(audioClipRefsSO.deliveryFail, orderCounter.transform.position, volumeConfig);
    }

    private void DeliveryManager_OnRecipeSuccess()
    {
        PlaySoundArray(audioClipRefsSO.deliverySuccess, orderCounter.transform.position, volumeConfig);
    }

    private void PlaySoundArray(AudioClip[] audioClipArray, Vector3 position, float volume)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }


    private void UpdateSFXVolume(float newVolume)
    {
        volumeConfig = newVolume;
    }

}
