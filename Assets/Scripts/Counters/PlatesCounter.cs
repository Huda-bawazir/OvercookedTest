using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;


    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlaterTimerMax = 4f;
    private int platesSpwanedAmount;
    private int platesSpwanedAmountMax = 4;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlaterTimerMax)
        {
            spawnPlateTimer = 0f;

            //spwan a demo ( a number amount of plates ) 
            if (KitchenGameManager.Instance.IsGamePlaying() && platesSpwanedAmount < platesSpwanedAmountMax)
            {
                platesSpwanedAmount++;
                OnPlateSpawned?.Invoke(this, new EventArgs());
            }
        }

    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {   //player is empty handed, so we can give them a plate 
            if(platesSpwanedAmount > 0)
            { // Then there is alteast one plate here, let's give it to the player 
                platesSpwanedAmount--; 
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, new EventArgs());
            }
            
        }
    }

}
