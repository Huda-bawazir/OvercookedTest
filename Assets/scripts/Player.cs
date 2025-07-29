using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    //making a local field to store id the player is walking
    //always calmel case foe fields
    private bool isWalking;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
        // Check if the player is pressing any of the WASD keys
        // If so, log "Processing!" to the console
        Vector2 inputvector = new Vector2(0, 0); 

        if (Input.GetKey(KeyCode.W))
        {
            inputvector.y = +1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputvector.x = -1;  
        }
        if (Input.GetKey(KeyCode.S))
        {   
            inputvector.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputvector.x = +1;
        }

        //normalize the vector to ensure consistent speed
       inputvector = inputvector.normalized;

        // Move the player based on the input vector
        Vector3 moveDirection = new Vector3(inputvector.x, 0f, inputvector.y);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

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

