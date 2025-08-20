using System;
using TMPro;
using UnityEngine;

public class GameStartCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountDownNumber;
    private const string NUMBER_POPUP = "numberPopUp"; 

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    //we need to listen to an event that fires off whenever the game state changes. 
    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnstateChanged;

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
        int countdownNumber = (int)Math.Ceiling(KitchenGameManager.Instance.GetCountdownToStartTimer()); 
        countdownText.text = countdownNumber.ToString();

        if(previousCountDownNumber != countdownNumber)
        {
            //update the previous countdown number and trigger the animation
            previousCountDownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound(); 

        }
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

