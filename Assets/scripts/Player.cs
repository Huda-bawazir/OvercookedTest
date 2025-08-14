using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;
using UnityEditor;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    //events for sounds 
    public event EventHandler OnPickedSomething; 
    //implementing singleton pattern using properties.
    //this is a property. properties are exactly how getters and setters work. 
    //you want other classes to be have access to the instance but not be able to set it. 
    public static Player Instance { get; private set; }//getters and setters

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform KitchenObjectHoldPoint;

    //making a local field to store id the player is walking
    //always camel case for fields
    private bool isWalking;
    //vector because...direction
    private Vector3 lastInteractDirection;
    //field to keep track of the selected counter
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    [Header("Capsule Settings:")]
    [SerializeField] public float radius = .5f;
    [SerializeField] public float height = .7f;

    private void Awake()
    {
        //if the instance has already been set to something then log an error. 
        if (Instance != null)
        {
            Debug.LogError("There is more than one player");

        }
        //there must be only one instance of the player.
        Instance = this;
    }


    //Event to change the counter into the selected couter prefab. This event handler takes in a generic 
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedcounterChange; //he capetalizes the names of the eventhandlers

    //Event Args. Is how you extend C# events to pass in some more data 
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        //they type of clear counter we are passing. 
        public BaseCounter selectedCounter;

    }

    private void Start()
    {
        //trigers the OninterAction Event handler in the GameInput class. 
        gameInput.OnInteractAction += GameInput_OnInteractAction;//Event handlers list
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;

    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        //if the game is not playing the code is going to stop excuting here. Same goes for the interaction. the  interactions will stop here.
        if(!KitchenGameManager.Instance.IsGamePlaying()){
            return;
        }
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying())
        {
            return;
        }
        //check if wwe have a selected counter 
        if (selectedCounter != null)
        {
            //if so we call the interact function
            selectedCounter.Interact(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
        HandleMovement();
        HandleInteractions();

    }

    //public because it is going to be accessed from another script (player)
    //here we are gonna resturn if the player is walking
    // for functions we have pascal case
    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        //Create a vector that takes in the player input
        //create a raycast the detects a collider from the player's tranform position and get information back from the raycast. 
        Vector2 inputVector2 = gameInput.GetMovementVector();

        Vector3 moveDirection = new Vector3(inputVector2.x, 0f, inputVector2.y);

        float interactionDistance = 2f;

        //RayCast always returns a boolean so in order to get the object's refrence, we must use a different kind dof raycast
        //A raycats with an out perameter called RaycastHit. They out keyword means it is an output Perameter it can return more than one value from a function
        //when the move direction is zero, we will be firing a Raycats towards no direction at all causing it not to hit anything 
        //because of that we need to keep track of the last moved direction. 
        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }
        //raycast towards any object with a collider 
        //set a game object to a certain layer and use a layer mask, then the raycast will hit objects in that layer. 
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactionDistance, countersLayerMask))
        {
            //when we find out that we hit something, the next best thing to do is to identify that thing/transform. 
            //Debug.Log(raycastHit.transform);

            //Try Get Componenet takes the type of component and it basically does the same thing as a rayCast (It returns a boolean) 
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //if true then the object has that component: ClearCounter. 
                if (baseCounter != selectedCounter)
                {
                    //calling the event to modify the selected counter 
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {

                //selectedCounter = null;
                SetSelectedCounter(null);
            }
        }
        else
        {
            //raycast isnt hitting an object.
            //selectedCounter = null;
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {

        //getting the input vector from the GameInput script 
        Vector2 inputVector2 = gameInput.GetMovementVector();
        // Move the player based on the input vector
        Vector3 moveDirection = new Vector3(inputVector2.x, 0f, inputVector2.y).normalized;


        //check if any object is in the way by firing a raycast
        float moveDistance = moveSpeed * Time.deltaTime;

        //returns a bool 
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * height, radius, moveDirection, moveDistance);

        if (!canMove)
        {
            //Cannot move towards moveDirection 

            //Attempt only x movement 
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = moveDirectionX.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * height, radius, moveDirectionX, moveDistance);
            if (canMove)
            {
                //can only move on the x
                moveDirection = moveDirectionX;
            }
            else
            {
                //cannot move only on X 

                //Atempt only Z movement 
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = moveDirectionZ.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * height, radius, moveDirectionZ, moveDistance);

                if (canMove)
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

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedcounterChange?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter

        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return KitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearkitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

}



