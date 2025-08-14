using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }
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
    private float gamePlayingToStart = 15f;



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
                }
                break;
            case State.CountingDownToStart:
                countdownToStart -= Time.deltaTime;
                if (countdownToStart < 0f)
                {
                    state = State.GamePlaying;
                }
                break;
            case State.GamePlaying:
                gamePlayingToStart -= Time.deltaTime;
                if (gamePlayingToStart < 0f)
                {
                    state = State.GameOver;
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
}
