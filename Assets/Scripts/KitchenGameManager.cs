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
    private float waitingToStart = 1f;
    private float countdownToStart = 3f;
    private float gamePlayingToStart = 150f;



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
                waitingToStart -= Time.deltaTime;
                if(waitingToStart < 0f)
                {
                    state = State.CountingDownToStart;
                    OnstateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.CountingDownToStart:
                countdownToStart -= Time.deltaTime;
                if (countdownToStart < 0f)
                {
                    state = State.GamePlaying;
                    OnstateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.GamePlaying:
                gamePlayingToStart -= Time.deltaTime;
                if (gamePlayingToStart < 0f)
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
        return countdownToStart; 
    }
}
