using UnityEngine;

public class GrillBurnFlashingBarUI : MonoBehaviour
{
    [SerializeField] private GrillCounter grillCounter;
     
    private Animator animator;
    private const string IS_FLASHING = "IsFlashing";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        grillCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_FLASHING, false);

    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventsArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = grillCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        animator.SetBool(IS_FLASHING, show); 
    }

}
