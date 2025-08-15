using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpwan;  
    public event EventHandler OnReceipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFaild;



    //A signleton of Delivery Manager
    public static DeliveryManager Instance { get; private set; }


    [SerializeField] private RecipeListSO recipeListSO;
    //Something to store the customer's orders in
    private List<RecipeSO> waitingRecipeSOList;

    //timer to spawn the recipes that are wating to be made 
    private float spawnRecipeTimer;
    private float spwanRecipeTimerMax = 6f;
    private int waitingRecipeMax = 4;

    //to keep track of the recipe's delivered
    private int successfulRecipeAmount; 

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        //counting downn the timie 
        spawnRecipeTimer -= Time.deltaTime;

        if(spawnRecipeTimer < 0f)
        {
           spawnRecipeTimer = spwanRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                spawnRecipeTimer = spwanRecipeTimerMax;
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpwan?.Invoke(this, EventArgs.Empty);   
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        //Do the ingredients of the plate match the item in the waiting manue recipe
        for(int i = 0; i < waitingRecipeSOList.Count; ++i)
        {
            RecipeSO waitngRecipeSO  = waitingRecipeSOList[i]; 

            if(waitngRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)//example : if the waiting list is the same legnth as the kitchen Object list in the burger for example
            {
                //has the same number of ingredients
                bool plateContentMatchesRecipe = true;

                //the second condition is that the ingredients of the plate and the recipe in the waiting list match
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitngRecipeSO.kitchenObjectSOList)
                {
                    //cycle throught the ingredients of the recipe to see if they match
                    bool ingredientFound = false; 
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycling throgh all the ingredients in the plate. 
                        //now we need to check if the ingredient on the plate matches the ingredients of the recipe 
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //Ingredient matches!
                            ingredientFound = true;
                            break;
                        }
                    }
                   if (!ingredientFound)
                    {
                        //this recipe ingredient was not found on the plate. 
                        plateContentMatchesRecipe = false;
                    }
                }
                if (plateContentMatchesRecipe)
                {
                    //player delivered the correct recipe!
                    successfulRecipeAmount++; 
                    //remove the recipe from the list once it is submitted
                    waitingRecipeSOList.RemoveAt(i);

                    OnReceipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    //to stop the recipe execution 
                    return;
                }
            }
        }

        //No matches found!
        //the player did not deliver a correct recipe
        OnRecipeFaild?.Invoke(this, EventArgs.Empty);   
    }
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList; 
    }
    public int GetSuccessfulRecipeAmount()
    {
        return successfulRecipeAmount;
    }
}
