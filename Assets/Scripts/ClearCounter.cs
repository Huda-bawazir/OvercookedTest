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
                //The player is cating something 
            }else
            {
                //player is not arying something 
                GetKitchenObject().SetKitchenObjectParent(player); 
            }
        }
    }

    
} 

