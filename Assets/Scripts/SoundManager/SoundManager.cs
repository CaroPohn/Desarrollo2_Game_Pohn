using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Tooltip("Reference to the DeliveryManager script.")]
    [SerializeField] DeliveryManager deliveryManager;

    [Tooltip("Reference to the OrderCounter script.")]
    [SerializeField] OrderCounter orderCounter;

    private float volumeConfig = 1f;

    [Tooltip("Reference to the ScriptableObject holding audio clips.")]
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

    /// <summary>
    /// Event handler for the player step event.
    /// Plays footstep sounds at the player's position.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void PlayerSounds_OnPlayerStep(object sender, System.EventArgs e)
    {
        PlayerSounds playerStep = sender as PlayerSounds;
        PlaySoundArray(audioClipRefsSO.footstep, playerStep.transform.position, volumeConfig);
    }

    /// <summary>
    /// Event handler for the trash counter event.
    /// Plays trash sounds at the trash counter's position.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySoundArray(audioClipRefsSO.trash, trashCounter.transform.position, volumeConfig);
    }

    /// <summary>
    /// Event handler for the base counter event.
    /// Plays object drop sounds at the base counter's position.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySoundArray(audioClipRefsSO.objectDrop, baseCounter.transform.position, volumeConfig);
    }

    /// <summary>
    /// Event handler for the interact event.
    /// Plays object pickup sounds at the player's interact position.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void Interact_OnPickedSomething(object sender, System.EventArgs e)
    {
        Interact playerInteract = sender as Interact;
        PlaySoundArray(audioClipRefsSO.objectPickup, playerInteract.transform.position, volumeConfig);
    }

    /// <summary>
    /// Event handler for the cutting counter event.
    /// Plays chopping sounds at the cutting counter's position.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySoundArray(audioClipRefsSO.chop, cuttingCounter.transform.position, volumeConfig);
    }

    /// <summary>
    /// Event handler for the recipe failed event.
    /// Plays delivery fail sounds at the order counter's position.
    /// </summary>
    private void DeliveryManager_OnRecipeFailed()
    {
        PlaySoundArray(audioClipRefsSO.deliveryFail, orderCounter.transform.position, volumeConfig);
    }

    /// <summary>
    /// Event handler for the recipe success event.
    /// Plays delivery success sounds at the order counter's position.
    /// </summary>
    private void DeliveryManager_OnRecipeSuccess()
    {
        PlaySoundArray(audioClipRefsSO.deliverySuccess, orderCounter.transform.position, volumeConfig);
    }

    /// <summary>
    /// Plays a random audio clip from an array at a specific position with given volume.
    /// </summary>
    /// <param name="audioClipArray">Array of audio clips to choose from.</param>
    /// <param name="position">Position where the sound will play.</param>
    /// <param name="volume">Volume of the sound.</param>
    private void PlaySoundArray(AudioClip[] audioClipArray, Vector3 position, float volume)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    /// <summary>
    /// Plays a specific audio clip at a given position with specified volume.
    /// </summary>
    /// <param name="audioClip">Audio clip to play.</param>
    /// <param name="position">Position where the sound will play.</param>
    /// <param name="volume">Volume of the sound.</param>
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    /// <summary>
    /// Updates the volume configuration for sound effects.
    /// </summary>
    /// <param name="newVolume">New volume value.</param>
    private void UpdateSFXVolume(float newVolume)
    {
        volumeConfig = newVolume;
    }

}
