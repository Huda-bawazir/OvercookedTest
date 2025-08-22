using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "skewerSO")]
public class SkewerSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSOList; 
    public string recipeName;   
}
