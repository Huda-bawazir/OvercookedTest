using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;



    private void Start()
    { 

        KitchenGameManager.Instance.OnGamePause += KitchenGameManager_OnGamePause;
        KitchenGameManager.Instance.OnGameUnpause += KitchenGameManager_OnGameUnpause;

        Hide(); 
    }

    private void KitchenGameManager_OnGameUnpause(object sender, System.EventArgs e)
    {
        Hide(); 
    }

    private void KitchenGameManager_OnGamePause(object sender, System.EventArgs e)
    {
        Show(); 
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
