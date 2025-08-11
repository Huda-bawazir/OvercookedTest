using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{

    //replacing a method that was already defined in a base (parent) class or interface, with a new version in this child class
    public override void Interact(Player player)
    {
        //will only serve to pick up and place items
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            //if there is no ibject then check the player himself, if he has object. 
           if ( player.HasKitchenObject())
            {
                //player is carrying something, drop the object from the player to the counter 
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else
            {
                //player has nothing 
            }
        }
        else
        {
            //there is a kitchen object there
            if ( player.HasKitchenObject())
            {
                //The player is carying a somthing
               //check if the plater is carying a plate. 
               if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {  //player is holding a plate
                    //add it to the list in PlateKitchenObject.
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        //destroy it from the kitchen counter to add it to the plate 
                        GetKitchenObject().DestroySelf();
                    }
                } else
                {
                    //Player is not holding a plate but something else. 
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //If the counter is hodling a plate \
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }

                }
            }
            else
            {
                //player is not arying something 
                GetKitchenObject().SetKitchenObjectParent(player); 
            }
        }
    }

    
} 

