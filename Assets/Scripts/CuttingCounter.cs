using UnityEngine;

public class CuttingCounter : BaseCounter
{
    //for the cut kitchen object
    [SerializeField] CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        //will only serve to pick up and place items
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            //if there is no ibject then check the player himself, if he has object. 
            if (player.HasKitchenObject())
            {
                //player is carrying something, drop the object from the player to the counter 
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
        //if there is a kitcjen object.
        if (HasKitchenObject())
        {
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO()); 
            //cut kitchen object. In order to cut kitchen object in the simplest way is to destroy it
            GetKitchenObject().OnDestroySelf();
            //Find out which recipe is applied,by cycling through the array to find the object in GetKitchenObject,and then spawn the recipe. 
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }else
        {
            Debug.Log("HasKitchenObject: " + HasKitchenObject());
            Debug.Log("KitchenObject: " + GetKitchenObject());
            Debug.Log("KitchenObjectSO: " + GetKitchenObject().GetKitchenObjectSO());

        }
    }

    //functoion to cycle through the Cutting recipes 
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObejctSO) {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input == inputKitchenObejctSO)
            {
                return cuttingRecipeSO.output; 
            }
        } return null;
    }
}

