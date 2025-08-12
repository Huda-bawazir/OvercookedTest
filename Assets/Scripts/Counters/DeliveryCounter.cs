using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    { //check if the player has an Object
        if (player.HasKitchenObject())
        {
            //the counter will only take plates. 
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //for now the object will be destroyed
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
