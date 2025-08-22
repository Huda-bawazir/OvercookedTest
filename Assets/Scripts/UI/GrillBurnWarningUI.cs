using UnityEngine;

public class GrillBurnWarningUI : MonoBehaviour
{

    [SerializeField] private GrillCounter grillCounter;

    private void Start()
    {
        grillCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        Hide(); 
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventsArgs e)
    {
        float burnShowProgressAmount = .5f; 
        bool show = grillCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        if (show)
        {
            Show(); 
        } else
        {
            Hide(); 
        }
    }

    private void Show() { gameObject.SetActive(true); }
    private void Hide() { gameObject.SetActive(false); }
}
