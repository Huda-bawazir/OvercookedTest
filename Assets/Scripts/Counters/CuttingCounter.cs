using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress 
{
    //need somekind o event to update bar imagie 
    public event EventHandler <IHasProgress.OnProgressChangedEventsArgs> OnProgressChanged;  
    
    public event EventHandler OnCut; 

    //for the cut kitchen object
    [SerializeField] CuttingRecipeSO[] cuttingRecipeSOArray;
    //kepping track of the cutting progress
    [SerializeField] private int cuttingProgress; 
    public override void Interact(Player player)
    {
        //will only serve to pick up and place items
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            //if there is no ibject then check the player himself, if he has object. 
            if (player.HasKitchenObject())
            {
                if (HasRecepieWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player is carying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs()
                    {
                        progressNormalized = (float) cuttingProgress/cuttingRecipeSO.progressCuttingMax
                    });
                }
                //player is carrying something, drop the object from the player to the counter 
            }
            else
            {
                //player has nothing 
            }
        }
        else
        {
            //there is a kitchen object there
            if (player.HasKitchenObject())
            {
                //The player is carying something 
            }
            else
            {
                //player is not carying something 
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }
    public override void InteractAlternate(Player player)
    {
        //if there is a kitchen object and it can be cut. 
        if (HasKitchenObject() && HasRecepieWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //increase cutting ptogress 
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs()
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.progressCuttingMax
            });
            if (cuttingProgress >= cuttingRecipeSO.progressCuttingMax)
            {
                //spawn the clices oncfe the object reaches the max progress.
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                //cut kitchen object. In order to cut kitchen object in the simplest way is to destroy it
                GetKitchenObject().OnDestroySelf();
                //Find out which recipe is applied,by cycling through the array to find the object in GetKitchenObject,and then spawn the recipe. 
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    //function to check if the object the player is carrying is a scriptable object recipe ( a part of recipe CuttingRecipeSO
    private bool HasRecepieWithInput( KitchenObjectSO inputKitchenObejctSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObejctSO);
        return cuttingRecipeSO;  
    }

    //functoion tp retun the output of the recipe 
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObejctSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObejctSO);
         if( cuttingRecipeSO != null)
         {
             return cuttingRecipeSO.output;
         } else
         {
                return null;
         }
    }
     
    //Function to cycle through the array and retun the kitchenobject that matches the one in the recipe. 
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                //if the kitchenObjectSO is a part of the cuttingrecipeSO
                return cuttingRecipeSO;
            }

            //its not a cutting recipe SO
        }
        return null;
    }
}

