using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrderTable : BaseCounter
{
    public string sceneName;

    public override void Interact(Interact playerInteract)
    {
        if (playerInteract.HasKitchenObject() && playerInteract.GetKitchenObject().kitchenObjectSO.objectName == kitchenObjectSO.objectName)
        {
            Destroy(playerInteract.GetKitchenObject().gameObject);
            playerInteract.SetKitchenObject(null);

            SceneManager.LoadScene(sceneName);
        }
    }
}
