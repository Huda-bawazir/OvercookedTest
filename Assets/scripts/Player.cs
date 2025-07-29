using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

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
        transform.forward = moveDirection;


        //two methods that work. 
        // transform.eulerAngles
        //transform.LookAt

        // Rotate the player to face the direction of movement
        //the funniest line ever!! does well for a horror game
        //transform.rotation = Quaternion.LookRotation(moveDirection)


    }
}

