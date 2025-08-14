using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter; 
    private AudioSource audiosource;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnstateChangedEventsArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried; 
        if (playSound)
        {
            audiosource.Play();
        } else
        {
            audiosource.Pause();
        }
    }
}
