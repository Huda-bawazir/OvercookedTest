using UnityEngine;

public class CuttingCounter : BaseCounter
{
    //for the cut kitchen object
    [SerializeField] KitchenObjectSO cutKitchenObjectSO;
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
                //The player is cating something 
            }
            else
            {
                //player is not arying something 
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }
    public override void InteractAlternate(Player player)
    {
        //if there is a kitcjen object.
        if (HasKitchenObject())
        {
            //cut kitchen object. In order to cut kitchen object in the simplest way is to destroy it
            GetKitchenObject().OnDestroySelf();
            //spawn the sliced object.
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}

