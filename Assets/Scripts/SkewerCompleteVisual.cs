using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class SkewerCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    } 

    [SerializeField] private SkewerKitchenObject skewerKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Start()
    {
        skewerKitchenObject.OnIngredientAdded += SkewerKitchenObject_OnIngredientAdded;
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
                kitchenObjectSOGameObject.gameObject.SetActive(false);
    }

    private void SkewerKitchenObject_OnIngredientAdded(object sender, SkewerKitchenObject.OnIngredientAddedEventArgs e)
    {
        // to find which object to turn on and off We must find a common link between a kitchenObjectSO and a game Object
        //we do that by cycling throgh the list
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
   
}
