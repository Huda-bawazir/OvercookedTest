    using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpwan += DeliveryManager_OnRecipeSpwan;
        DeliveryManager.Instance.OnReceipeCompleted += DeliveryManager_OnReceipeCompleted; 

        UpdateVisual();

    }

    private void DeliveryManager_OnReceipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpwan(object sender, System.EventArgs e)
    {
        UpdateVisual(); 
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
       foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
          Transform recipeTransform =  Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true); 
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO); 
        }
    }
}
