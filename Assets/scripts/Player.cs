using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;

    //making a local field to store id the player is walking
    //always calmel case foe fields
    private bool isWalking;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
        //getting the input vector from the GameInout script 
        Vector2 inputVector2 = gameInput.GetMovementVector(); 
        // Move the player based on the input vector
        Vector3 moveDirection = new Vector3(inputVector2.x, 0f, inputVector2.y);
        

        //check if any object is in the way by firing a raycast
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = .7f;
        //returns a bool 
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance); 
        
        if (!canMove)
        {
            //Cannot move towards moveDirection 

            //Attempt only x movement 
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);
            
            if (canMove)
            {
                //can only move on the x
                moveDirection = moveDirectionX;
            }
            else
            {
                //cannot move only on X 

                //Atempt only Z movement 
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if(canMove)
                {
                    //can only move z 
                    moveDirection = moveDirectionZ;
                }
                else
                {
                    //cannot move in any direction

                }


            }

        }
        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        //Math function called lurp/slerp, it helsps with the interpolating 
        float rotateSpeed = 10f; 
        //interpolate between and b based on t
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
         
        isWalking = moveDirection != Vector3.zero;

        //two methods that work. 
        // transform.eulerAngles
        //transform.LookAt
    }

    //public because it is going to be accessed from another script (player)
    //here we are gonna resturn if the player is walking
    // for functions we have pascal case
    public bool IsWalking()
    {
        return isWalking; 
    }
}

