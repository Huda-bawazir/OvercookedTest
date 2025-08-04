using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //refrence for tomato Prefab
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   public void Interact()
    {
        Debug.Log("Interact");

        //spawining tomato 
        Transform kitchenObjectTransform  = Instantiate(kitchenObjectSO.Prefab,counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().ObjectName);



    }
} 