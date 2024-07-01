using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Manages the delivery and completion of recipes in the game.
/// </summary>
public class DeliveryManager : MonoBehaviour
{
    [Tooltip("Reference to the ScriptableObject containing all recipes.")]
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList = new List<RecipeSO>();

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

    private int waitingRecipesMax = 4;
    private int successfulRecipesAmount;

    /// <summary>
    /// Event triggered when a new recipe is spawned.
    /// </summary>
    public event Action OnRecipeSpawned;

    /// <summary>
    /// Event triggered when a plate has all the correct ingredients for a recipe.
    /// </summary>
    public event Action OnRecipeCompleted;

    /// <summary>
    /// Event triggered when a recipe is successfully delivered.
    /// </summary>
    public event Action OnRecipeSuccess;

    /// <summary>
    /// Event triggered when a recipe fails to match.
    /// </summary>
    public event Action OnRecipeFailed;

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;

        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke();
            }
        }
    }

    /// <summary>
    /// Delivers a plate to match against waiting recipes and completes if successful.
    /// </summary>
    /// <param name="plateKitchenObject">The plate object containing ingredients to deliver.</param>
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectsSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;

                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectsSOList)
                {
                    bool ingredientFound = false;

                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if(!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }

                    if(plateContentsMatchesRecipe)
                    {
                        successfulRecipesAmount++;
                        
                        waitingRecipeSOList.RemoveAt(i);

                        OnRecipeCompleted?.Invoke();
                        OnRecipeSuccess?.Invoke();
                        return;
                    }
                }
            }
        }

        OnRecipeFailed?.Invoke();
    }

    /// <summary>
    /// Retrieves the list of waiting recipes.
    /// </summary>
    /// <returns>The list of waiting RecipeSO objects.</returns>
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    /// <summary>
    /// Retrieves the count of successfully completed recipes.
    /// </summary>
    /// <returns>The count of successful recipes.</returns>
    public int GetSuccesfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
}
