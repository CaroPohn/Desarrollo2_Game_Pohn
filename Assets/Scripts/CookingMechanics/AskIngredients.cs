using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskIngredients : BaseCounter
{
    [SerializeField] private List<KitchenObject> objects;
    private bool[] checkItems;

    [SerializeField] private GameObject tomatoSlice;
    [SerializeField] private GameObject lettuceSlice;
    [SerializeField] private GameObject cheeseSlice;
    [SerializeField] private GameObject twoBuns;
    [SerializeField] private GameObject cookedBurguer;

    List<GameObject> items = new List<GameObject>();

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
                if(playerInteract.GetKitchenObject().kitchenObjectSO.objectName == objects[i].kitchenObjectSO.objectName && !checkItems[i])
                {
                    checkItems[i] = true;
                    Destroy(playerInteract.GetKitchenObject().gameObject);
                    playerInteract.SetKitchenObject(null);

                    switch(objects[i].kitchenObjectSO.objectName)
                    {
                        case "Bun":
                            items.Add(Instantiate(twoBuns, counterTopPoint));
                            break;
                        case "Tomato":
                            items.Add(Instantiate(tomatoSlice, counterTopPoint));
                            break;
                        case "Cheese Block":
                            items.Add(Instantiate(cheeseSlice, counterTopPoint));
                            break;
                        case "Lettuce":
                            items.Add(Instantiate(lettuceSlice, counterTopPoint));
                            break;
                        case "Burger":
                            items.Add(Instantiate(cookedBurguer, counterTopPoint));
                            break;
                        default:
                            break;
                    }

                    break;
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
                if(!HasKitchenObject())
                {
                    foreach(GameObject item in items) 
                    {
                        Destroy(item);
                    }

                    Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
                    kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

                    return;
                }

                if (HasKitchenObject())
                {
                    if (!playerInteract.HasKitchenObject())
                    {
                        GetKitchenObject().SetKitchenObjectParent(playerInteract);
                    }
                }
            }
        }
    }
}
