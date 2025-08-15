using System;
using TMPro;
using UnityEngine;

public class GameStartCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;


    //we need to listen to an event that fires off whenever the game state changes. 
    private void Start()
    {
        KitchenGameManager.Instance.OnstateChanged += KitchenGameManager_OnstateChanged;

        Hide(); 
    }

    private void KitchenGameManager_OnstateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Show(); 
        } else
        {
            Hide(); 
        }
    }
    private void Update()
    {
        countdownText.text = Math.Ceiling(KitchenGameManager.Instance.GetCountdownToStartTimer()).ToString();
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

