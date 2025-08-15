using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnstateChanged; 
   private enum State
    {
        WaitingToStart, 
        CountingDownToStart,
        GamePlaying, 
        GameOver,
   }
    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer; 
    private float gamePlayingTimerMax = 10f;



    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0f)
                {
                    state = State.CountingDownToStart;
                    OnstateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.CountingDownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax; 
                    OnstateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnstateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
        Debug.Log(state);
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
}
