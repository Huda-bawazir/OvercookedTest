using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
   //fields 
    private Animator animator;
    [SerializeField] private Player player;

    //to protect against spelling mistakes
    private const string IS_WALKING = "IsWalking";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }



}
