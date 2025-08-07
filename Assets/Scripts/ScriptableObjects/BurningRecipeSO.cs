using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    //A scriptable object to hold a refrence for the regular object and the cut, so then we can identify the input(regular object ex.tomato) so that we can spawn the output( sliced object ex.tomato slices)
    public KitchenObjectSO input; 
    public KitchenObjectSO output;

    //we need to know if the object has been cut, so we need to define some kind of maximum. 
    public float burningProgressMax; 


}