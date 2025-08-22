using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance {  get; private set; } 

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton; 
    [SerializeField] private Button moveUpButton; 
    [SerializeField] private Button moveDownButton; 
    [SerializeField] private Button moveLeftButton; 
    [SerializeField] private Button moveRightButton; 
    [SerializeField] private Button interactButton; 
    [SerializeField] private Button interactAltButton; 
    [SerializeField] private Button pauseButton; 
    [SerializeField] private Button GamePadInteract; 
    [SerializeField] private Button GamePadInteractAlternate; 
    [SerializeField] private Button GamePadPause; 
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;     
    [SerializeField] private TextMeshProUGUI moveUpText;     
    [SerializeField] private TextMeshProUGUI moveDownText;     
    [SerializeField] private TextMeshProUGUI moveLeftText;     
    [SerializeField] private TextMeshProUGUI moveRightText;     
    [SerializeField] private TextMeshProUGUI interactText;     
    [SerializeField] private TextMeshProUGUI interactAltText;     
    [SerializeField] private TextMeshProUGUI PauseText;
    [SerializeField] private TextMeshProUGUI GamePadInteractText;
    [SerializeField] private TextMeshProUGUI GamePadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI GamePadPauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;

     private Action onCloseButtonAction;

    private void Awake()
    {
        Instance = this;
        soundEffectButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume(); 
            UpdateVisuals();

        });

        musicButton.onClick.AddListener(() => { 
            MusicManager.Instance.ChangeVolume();
            UpdateVisuals();

        });
        closeButton.onClick.AddListener(() => {
            Hide();
            onCloseButtonAction();
        });
        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up);});
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down);});
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left);});
        moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right);});
        interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact);});
        interactAltButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.InteractAlternate);});
        pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause);});
        GamePadInteract.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePad_Interact);});
        GamePadInteractAlternate.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePad_InteractAlternate);});
        GamePadPause.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePad_Pause);});
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnpause += KitchenGameManager_OnGameUnpause;
        UpdateVisuals();

        HidePressToRebindKey();
        Hide(); 
    }

    private void KitchenGameManager_OnGameUnpause(object sender, System.EventArgs e)
    {
        Hide();  
    }

    private void UpdateVisuals()
    {
        soundEffectText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10);

        //for key rebinding 
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        PauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        GamePadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Interact);
        GamePadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_InteractAlternate);
        GamePadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Pause);

    }
    public void Show(Action onCloseeButtonAction)
    {
        this.onCloseButtonAction = onCloseeButtonAction;
        gameObject.SetActive(true);
        //if it doesnt select in the options
        soundEffectButton.Select();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey() { pressToRebindKeyTransform.gameObject.SetActive(true); }
    private void HidePressToRebindKey() { pressToRebindKeyTransform.gameObject.SetActive(false); }

    private void RebindBinding (GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisuals();
        });
        
    }

}
