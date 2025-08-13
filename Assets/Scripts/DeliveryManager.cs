using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private RecipeListSO recipeListSO;
    //Something to store the customer's orders in
    private List<RecipeSO> waitingRecipeSOList;

    //timer to spawn the recipes that are wating to be made 
    private float spawnRecipeTimer;
    private float spwanRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4; 

    private void Awake()
    {
        spawnRecipeTimer = spwanRecipeTimerMax; 
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        //counting downn the timie 
        spawnRecipeTimer -= Time.deltaTime;

        if(spawnRecipeTimer < 0f)
        {
            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                spawnRecipeTimer = spwanRecipeTimerMax;
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.name);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }
}
