using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    //enum can be used to define states. 
    private enum State
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
    private BurningRecipeSO BurningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            Debug.Log(fryingTimer);
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer > fryingRecipeSO.fryingProgressMax)
                    {
                        //fried
                        GetKitchenObject().OnDestroySelf();

                        //spwan the cooked object. 
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);


                        //Modify the state
                        state = State.Fried;
                        burningTimer = 0f;

                        BurningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    }
                    break;
                case State.Fried:
                    burningTimer  += Time.deltaTime;
                    if (burningTimer > BurningRecipeSO.burningProgressMax)
                    {
                        //fried
                        GetKitchenObject().OnDestroySelf();

                        //spwan the cooked object. 
                        KitchenObject.SpawnKitchenObject(BurningRecipeSO.output, this);

                        //Modify the state
                        state = State.Burned;
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
                }
                //player is carrying something, drop the object from the player to the counter 
            }
            else
            {
                //player has nothing 
            }
        }
        else
        {
            //there is a kitchen object theres
            if (player.HasKitchenObject())
            {
                //The player is carying something 
            }
            else
            {
                //player is not carying something 
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
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
}
