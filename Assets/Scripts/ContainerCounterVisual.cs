using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    //making a constant so that dealing with strings become better
    private string OPEN_CLOSE = "OpenClose"; 
     //refrence needed for when the player grabs the object from the countainer  
    [SerializeField] private ContainerCounter containercounter; 

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    //on start because we are using an external refrence
    private void Start()
    {
        containercounter.OnPlayerGrabbedObject += Containercounter_OnPlayerGrabbedObject;
        
    }

    private void Containercounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        //when working with animator perametors we must work with strings 
        animator.SetTrigger(OPEN_CLOSE);
    }
}
