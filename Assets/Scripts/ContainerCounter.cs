using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    //an event handler that is going to fire when the player grabs an object.
    public event EventHandler OnPlayerGrabbedObject; 
        
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        //if kitchen object is null we are going to spawn it
        if (!player.HasKitchenObject())
        {   //player does not have an object.  
            //spawining a prefab that has a scriptable object.
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            //when the player grabs the object we invoke the event
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        } 
    }
}
