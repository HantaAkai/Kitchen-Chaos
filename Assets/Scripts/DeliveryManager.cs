using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    //This is a singleton part 1
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;


    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;


    private void Awake() {
        waitingRecipeSOList = new List<RecipeSO>();

        //This is a singleton part 2
        Instance = this;
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }


    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++ ) { 
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOLists().Count) {
                //Has the same number of ingredients
                bool plateContentMatchesRecipe = true;

                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    //Cycling through all ingredients in the Recipe

                    bool ingredientFound = false;

                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOLists()) {
                        //Cycling through all ingredients on the Plate

                        if (plateKitchenObjectSO == recipeKitchenObjectSO) { 
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        //This ingredient was not foun on the Plate
                        plateContentMatchesRecipe = false;
                    }
                }

                if (plateContentMatchesRecipe) {
                    //Player delivered the correct Recipe
                    Debug.Log("Player delivered the correct Recipe!");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }

        //No match recipe was found
        //Plaeyr did not deliver a correct Recipe
        Debug.Log("Plaeyr did not deliver a correct Recipe");
    }

}