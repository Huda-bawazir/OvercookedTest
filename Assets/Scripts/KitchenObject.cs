using System.Security.Cryptography;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    //need a moonbehavior script to get the scripable object and be able to attache it to the prefab 
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    //to helpe make sure the kitchen object knows where it is. 
    private IKitchenObjectParent kitchenObjectParent; 

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public void SetKitchenObjectParent (IKitchenObjectParent kitchenObjectParent)
    {
        //clear the current parent if its not null
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearkitchenObject(); 
        }

        //set the kitchen object for the parent. 
        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject() ) {

            Debug.LogError("Counter already has a kitchenObject"); 
        }
        kitchenObjectParent.SetKitchenObject(this); 

        //When setting the lcear counter to a differnt one, the object should automatically transport. 
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitcheObjectParent ()
    {
        return kitchenObjectParent;
    }

    public void OnDestroySelf()
    {
        kitchenObjectParent.ClearkitchenObject();
        Destroy(gameObject);
    }
}
