using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText; 
    [SerializeField] private TextMeshProUGUI keyMoveDownText; 
    [SerializeField] private TextMeshProUGUI keyMoveLeftText; 
    [SerializeField] private TextMeshProUGUI keyMoveRightText; 
    [SerializeField] private TextMeshProUGUI keyInteract; 
    [SerializeField] private TextMeshProUGUI keyInteractAlt; 
    [SerializeField] private TextMeshProUGUI keyPause; 
    [SerializeField] private TextMeshProUGUI keyGamePadInteractText; 
    [SerializeField] private TextMeshProUGUI keyGamePadInteractAlt; 
    [SerializeField] private TextMeshProUGUI keyGamePadPauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        UpdateVisual();
        Show(); 

    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }


    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keyInteract.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAlt.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        keyPause.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

        //keyGamepads 
        //keyGamePadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Puase);
        // keyGamePadInteractAlt.text = GameInput.Instance.GetBindingText(GameInput.Binding.Puase);
        //keyGamePadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Puase);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

