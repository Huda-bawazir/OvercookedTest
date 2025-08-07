using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    //making a constant so that dealing with strings become better
    private string CUT = "Cut";
    //refrence needed for when the player grabs the object from the countainer  
    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //on start because we are using an external refrence
    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;

    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}