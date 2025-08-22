using System;
using UnityEngine;

public class GrillCounter : BaseCounter, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventsArgs> OnProgressChanged;

    public event EventHandler<OnstateChangedEventsArgs> OnStateChanged;
    public class OnstateChangedEventsArgs : EventArgs
    {
        public State state;
    }

    //enum can be used to define states. 
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;


    private State state;
    private float fryingTimer;
    private float burningTimer;

    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    //firing off the event. 
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingProgressMax,
                    });
                    if (fryingTimer > fryingRecipeSO.fryingProgressMax)
                    {
                        //fried
                        GetKitchenObject().DestroySelf();

                        //spwan the cooked object. 
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);


                        //Modify the state
                        state = State.Fried;
                        burningTimer = 0f;

                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnstateChangedEventsArgs
                        {
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    //firing off the event
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningProgressMax,
                    });
                    if (burningTimer > burningRecipeSO.burningProgressMax)
                    {
                        //fried
                        GetKitchenObject().DestroySelf();

                        //spwan the cooked object. 
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        //Modify the state
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnstateChangedEventsArgs
                        {
                            state = state
                        });

                        //make sure the bar hides itself when its goes into burned
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }
    public override void Interact(Player player)
    {
        //will only serve to pick up and place items
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            //if there is no ibject then check the player himself, if he has object. 
            if (player.HasKitchenObject())
            {
                if (HasRecepieWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player is carying something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    //when the player drops something that can be friend Set it to a different state as well as reset a timer
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnstateChangedEventsArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingProgressMax,
                    });
                }
                //player is carrying something, drop the object from the player to the counter 
            }
            else
            {
                //player not carrying anything 
            }
        }
        else
        {
            //there is a kitchen object here
            if (player.HasKitchenObject())
            {
                //The player is carying something 
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {  //player is holding a plate
                    //add it to the list in PlateKitchenObject.
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        //destroy it from the kitchen counter to add it to the plate 
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnstateChangedEventsArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                //player is not carying something 
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnstateChangedEventsArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs
                {
                    progressNormalized = 0f
                }); ;
            }
        }

    }

    //function to check if the object the player is carrying is a scriptable object recipe ( a part of recipe CuttingRecipeSO
    private bool HasRecepieWithInput(KitchenObjectSO inputKitchenObejctSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObejctSO);
        return fryingRecipeSO;
    }

    //functoion tp retun the output of the recipe 
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObejctSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObejctSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    //Function to cycle through the array and retun the kitchenobject that matches the one in the recipe. 
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                //if the kitchenObjectSO is a part of the cuttingrecipeSO
                return fryingRecipeSO;
            }

            //its not a cutting recipe SO
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                //if the kitchenObjectSO is a part of the cuttingrecipeSO
                return burningRecipeSO;
            }

            //its not a cutting recipe SO
        }
        return null;

    }

    public bool IsFried()
    {
        return state == State.Fried;
    }
 
} 

