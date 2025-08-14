using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    //all the common code is written herer in this class 

    [SerializeField] private Transform counterTopPoint;

    public static event EventHandler OnAnyPlacedObjectHere; 

    private KitchenObject kitchenObject;

    // for every function we want the child to implement in their own way. 

    public virtual void Interact (Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate (Player player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate"); 
    }

    //functions the different type of counters inheret. 
    //whenever we move to a second parent, we must ask the second parent to return the countertopoint and move it there
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnAnyPlacedObjectHere?.Invoke(this, EventArgs.Empty);
        }
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

