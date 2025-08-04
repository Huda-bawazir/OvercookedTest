using System.Security.Cryptography;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    //need a moonbehavior script to get the scripable object and be able to attache it to the prefab 
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    //to helpe make sure the kitchen object knows where it is. 
    private ClearCounter clearCounter; 

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public void SetClearCounter (ClearCounter clearCounter)
    {
        this.clearCounter = clearCounter;
        //When setting the lcear counter to a differnt one, the object should automatically transport. 
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter ()
    {
        return clearCounter;
    }


}
