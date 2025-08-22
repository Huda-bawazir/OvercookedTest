using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class KitchenObject : MonoBehaviour
{
    //need a moonbehavior script to get the scripable object and be able to attache it to the prefab 
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    //to helpe make sure the kitchen object knows where it is. 
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        //clear the current parent if its not null
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearkitchenObject();
        }

        //set the kitchen object for the parent. 
        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {

            Debug.LogError("Counter already has a kitchenObject");
        }
        kitchenObjectParent.SetKitchenObject(this);

        //When setting the lcear counter to a differnt one, the object should automatically transport. 
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public IKitchenObjectParent GetKitcheObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearkitchenObject();
        Destroy(gameObject);
    }

    //verifying if an object is a plate. This function returns a bool and the playeKitchenObject.
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        //if this is a object is a plate kitchen Object. 
        if (this is PlateKitchenObject)
        {
            //if it is, assign the value as in cast it as a plate kitchen object.
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            //if it's not a plate return false
            return false;
        }
    }
    public bool TryGetSkewer(out SkewerKitchenObject skewerKitchenObject)
    {
        //if this is a object is a skewer kitchen Object. 
        if (this is SkewerKitchenObject)
        {
            //if it is, assign the value as in cast it as a skewer kitchen object.
            skewerKitchenObject = this as SkewerKitchenObject;
            return true;
        }
        else
        {
            skewerKitchenObject = null;
            //if it's not a plate return false
            return false;
        }
    }


    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        //this will autmoatically span the object and  set the parent. 
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab).transform;

        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}
