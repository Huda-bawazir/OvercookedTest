using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //refrence for tomato Prefab
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
  

    private KitchenObject kitchenObject;

    public void Interact(Player player)
    {
        //if kitchen object is null we are going to spawn it
        if (kitchenObject == null)
        { 
            //spawining tomato 
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }else
        {
            //Give the object to the player. 
            kitchenObject.SetKitchenObjectParent(player);
        }

    }

    //whenever we move to a second parent, we must ask the second parent to return the countertoppoint and move it there
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject (KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;   
    }

    public void ClearkitchenObject()
    {
        kitchenObject = null; 

    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
} 

