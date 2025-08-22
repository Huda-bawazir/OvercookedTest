using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePause; 
    public event EventHandler OnGameUnpause; 

   private enum State
    {
        WaitingToStart, 
        CountingDownToStart,
        GamePlaying,
        GameOver,
   }
    private State state;
    //modify it to start when the playyer interacts with the game.
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer; 
    public float gamePlayingTimerMax = 30f;
    private bool isGamePaused = false; 



    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;

    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountingDownToStart;
            OnStateChanged?.Invoke(this, new EventArgs());
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame(); 
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.CountingDownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax; 
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.GameOver:
                Invoke(nameof(RestartGame), 5f);
                break;
            default:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountingDownToStart; 
    }

    public bool IsGameOver()
    { 
        return state == State.GameOver; 
    }
    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer; 
    }

    public float GetGamePlayingTimerNormalized() {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);  
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty); 
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpause?.Invoke(this, EventArgs.Empty);   
        }
    }
    private void RestartGame()
    {
        Loader.Load(Loader.Scene.MainMenu); 
    }
}
