using UnityEngine;

public class SkewerIconUI : MonoBehaviour
{
    [SerializeField] private SkewerKitchenObject skwereKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
       iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        skwereKitchenObject.OnIngredientAdded += SkwereKitchenObject_OnIngredientAdded; ;
    }

    private void SkwereKitchenObject_OnIngredientAdded(object sender, SkewerKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        //Clean up the icons so that it doesnt end up spawning too many icons.
        foreach (Transform child in transform)
        {
            //do not destroy the icontemplate itself.
            if (child == iconTemplate) continue; 
            //this willl destroy all the previous children.
            Destroy(child.gameObject); 
        }
        foreach (KitchenObjectSO kitchenObjectSO in skwereKitchenObject.GetKitchenObjectSOList())
        {
           Transform iconTransform = Instantiate(iconTemplate, transform);
           iconTransform.gameObject.SetActive(true);   
           iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO); 
        }
    }
}
