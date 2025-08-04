using UnityEngine;

public interface IKitchenObjectParent 
{
    //An interface is essentially a contract where each class that implements that interface must fllow the contract 
    //you can define functions, properties, but not fields.
    //whenever we move to a second parent, we must ask the second parent to return the countertoppoint and move it there
    public Transform GetKitchenObjectFollowTransform();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void ClearkitchenObject();

    public bool HasKitchenObject(); 
}
