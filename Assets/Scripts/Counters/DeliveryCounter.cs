using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;    
    }
    public override void Interact(Player player)
    { //check if the player has an Object
        if (player.HasKitchenObject())
        {
            //the counter will only take plates. 
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //only accepts plates
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                //for now the object will be destroyed
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
