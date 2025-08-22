using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    //replacing a method that was already defined in a base (parent) class or interface, with a new version in this child class
    public override void Interact(Player player)
    {
        var kitchenObject = player.GetKitchenObject();
        //will only serve to pick up and place items
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            //if there is no ibject then check the player himself, if he has object. 
            if (player.HasKitchenObject())
            {
                //player is carrying something, drop the object from the player to the counter 
                kitchenObject.SetKitchenObjectParent(this);
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
                //The player is carying a somthing
                //check if the plater is carying a plate. 
                if (kitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {  
                    //player is holding a plate
                    //add it to the list in PlateKitchenObject.
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        //destroy it from the kitchen counter to add it to the plate 
                        GetKitchenObject().DestroySelf();
                    }
                }
                if (kitchenObject.TryGetSkewer(out SkewerKitchenObject skewerKitchenObject))
                {
                    //player is holding a skewer
                    //add it to the list in SkewerKitchenObject.
                    if (skewerKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        //destroy it from the kitchen counter to add it to the skewer 
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //Player is not holding a plate but something else. 
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //If the counter is hodling a plate 
                        if (plateKitchenObject.TryAddIngredient(kitchenObject.GetKitchenObjectSO()))
                        {
                            kitchenObject.DestroySelf();
                        }
                    }
                    else if (GetKitchenObject().TryGetSkewer(out skewerKitchenObject))
                    {
                        //If the counter is hodling a skewer 
                        if (skewerKitchenObject.TryAddIngredient(kitchenObject.GetKitchenObjectSO()))
                        {
                            kitchenObject.DestroySelf();
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

        ////Grilled Meat
        //if (player.HasKitchenObject())
        //{
        //    //The player is carying a somthing
        //    //check if the player is carying a skewer. 
        //    if (kitchenObject.TryGetSkewer(out SkewerKitchenObject skewerKitchenObject))
        //    {  //player is holding a skewer
        //       //add it to the list in skewerKitchenObject.
        //        if (skewerKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
        //        {
        //            //destroy it from the kitchen counter to add it to the plate 
        //            GetKitchenObject().DestroySelf();
        //        }
        //    }
        //    else
        //    {
        //        //Player is not holding a skewer but something else. 
        //        if (GetKitchenObject().TryGetSkewer(out skewerKitchenObject))
        //        {
        //            //If the counter is hodling a plate 
        //            if (skewerKitchenObject.TryAddIngredient(kitchenObject.GetKitchenObjectSO()))
        //            {
        //                kitchenObject.DestroySelf();
        //            }
        //        }

        //    }

        //}
        //else
        //{
        //    //player is not arying something 
        //    GetKitchenObject().SetKitchenObjectParent(player);
        //}
    }
}

