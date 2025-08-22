using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "skewerSO")]
public class skewerSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSOList; 
    public string recipeName;   
}
