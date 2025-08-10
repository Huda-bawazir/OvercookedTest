using UnityEngine;

public class PlatesCounter : BaseCounter
{
    private float spawnPlateTimer;
    private float spawnPlaterTimerMax = 4f;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO; 
    private void Update()
    {
        spawnPlateTimer += Time.time;
        if (spawnPlateTimer > spawnPlaterTimerMax)
        {
            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this); 
        }
    }
}
