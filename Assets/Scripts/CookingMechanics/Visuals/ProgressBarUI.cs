using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages a UI progress bar based on an object implementing IHasProgress.
/// </summary>
public class ProgressBarUI : MonoBehaviour
{
    [Tooltip("Reference to the GameObject that has progress.")]
    [SerializeField] private GameObject hasProgressGameObject;

    [Tooltip("Image component representing the progress bar.")]
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        
        if(hasProgress == null )
        {
            Debug.LogError("Game Object " + hasProgressGameObject + " does not have a component that implements IHasProgress!");
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        barImage.fillAmount = 0f;

        Hide();
    }

    /// <summary>
    /// Event handler for when the progress changes.
    /// Updates the fill amount of the progress bar and shows/hides based on progress value.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Event arguments containing the normalized progress value.</param>
    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if(e.progressNormalized ==  0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    /// <summary>
    /// Shows the progress bar UI.
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the progress bar UI.
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
