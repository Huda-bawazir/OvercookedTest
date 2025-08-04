using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    //need a moonbehavior script to get the scripable object and be able to attache it to the prefab 
    [SerializeField] private KitchenObjectSO kitchenObjectSO; 

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }
}
