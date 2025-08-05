using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //refrence for tomato Prefab

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    //replacing a method that was already defined in a base (parent) class or interface, with a new version in this child class
    public override void Interact(Player player)
    {
        //will only serve to pick up and place items

    }

    
} 

