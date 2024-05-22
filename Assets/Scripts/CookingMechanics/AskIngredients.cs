using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskIngredients : BaseCounter
{
    [SerializeField] private List<KitchenObject> objects;
    private bool[] checkItems;

    private void Start()
    {
        checkItems = new bool[objects.Count];
    }

    public override void Interact(Interact playerInteract)
    {
        if(playerInteract.HasKitchenObject())
        {
            for(int i = 0; i < objects.Count; i++)
            {
                if(playerInteract.GetKitchenObject().kitchenObjectSO.objectName == objects[i].kitchenObjectSO.objectName)
                {
                    checkItems[i] = true;
                    Destroy(playerInteract.GetKitchenObject().gameObject);
                    playerInteract.SetKitchenObject(null);
                }
            }
        }
        else if (!playerInteract.HasKitchenObject())
        {
            int validItemsCounter = 0;

            foreach(bool boolItem in checkItems) 
            { 
                if(boolItem)
                {
                    validItemsCounter++;
                }
            }
            if(validItemsCounter >= objects.Count) 
            {
                //TODO: devolver la hamburguesa 
                Debug.Log("Anvorguesa");
            }
        }
    }
}
