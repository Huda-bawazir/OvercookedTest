using UnityEngine;

public class GrillCounterSound : MonoBehaviour
{
    [SerializeField] private GrillCounter grillCounter; 
    private AudioSource audiosource;
    private float warningSoundTimer;
    private bool playWarningSound; 

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        grillCounter.OnStateChanged += GrillCounter_OnStateChanged;
        grillCounter.OnProgressChanged += GrillCounter_OnProgressChanged;
    }

    private void GrillCounter_OnStateChanged(object sender, GrillCounter.OnstateChangedEventsArgs e)
    {
        bool playSound = e.state == GrillCounter.State.Frying || e.state == GrillCounter.State.Fried;
        if (playSound)
        {
            audiosource.Play();
        }
        else
        {
            audiosource.Pause();
        }
    }

    private void GrillCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventsArgs e)
    {
       float burnShowProgressAmount = .5f;
       playWarningSound = grillCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

 

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlaywarningSound(grillCounter.transform.position);
            }
        }
    }
}
