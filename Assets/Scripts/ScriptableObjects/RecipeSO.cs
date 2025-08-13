using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectList; 
    public string recipeName;   
}
