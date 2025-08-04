using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //refrence for tomato Prefab
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Update()
    {
       if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
                string testing2 = "Object swapped"; 
                Debug.Log(kitchenObject.GetClearCounter());
            }
        }
    }

    public void Interact()
    {

        //if kitchen object is null we are going to spawn it
        if (kitchenObject == null)
        {
            //Debug.Log("Interact");

            //spawining tomato 
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;

            //fixes the issue of spawning infinite objects. 
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            //Tell the object where it lives(this ClearCounter)
            kitchenObject.SetClearCounter(this); 

             //chaning a paerent counter


            //Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().ObjectName);

        }else
        {
            string testing = "object on counter"; 
            Debug.Log(kitchenObject.GetClearCounter() + testing); 
        }

    }

    //whenever we move to a second parent, we must ask the second parent to return the countertoppoint and move it there
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
} 