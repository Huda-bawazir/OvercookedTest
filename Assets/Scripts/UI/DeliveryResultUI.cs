using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField] private Image backgrounImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManger_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFaild += DeliveryManger_OnRecipeFaild;
        gameObject.SetActive(false);
    }

    private void DeliveryManger_OnRecipeFaild(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgrounImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "DELIVERY\nFAILED"; 
    }

    private void DeliveryManger_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgrounImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCESS";
    }
}
