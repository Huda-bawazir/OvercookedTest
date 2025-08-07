using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private List<GameObject> selectedGameObjects;

    //instead of using a seralized field the way we did for gameinput in the Player's script.
    //we can use singlton pattern. 
    private void Start()
    {
        //in start so that it runs after the script where the instance runs. 
        Player.Instance.OnSelectedcounterChange += Player_OnSelectedCounterChange;
        for (int i = 0;i< transform.childCount;i++)
            selectedGameObjects.Add(transform.GetChild(i).gameObject);
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangedEventArgs e) 
    {
        //compare which counter this visual belongs to. 
        if (e.selectedCounter == baseCounter)
        {
            //if the selected counter is this counter then show the visual 
            Show(); 
        }
        else
        {
            //if now then hide 
            Hide();
        }
    }

    private void Show()
    {
        foreach (var selectedGameObject in selectedGameObjects)
            selectedGameObject.SetActive(true);
    }

    private void Hide()
    {
        foreach (var selectedGameObject in selectedGameObjects)
            selectedGameObject.SetActive(false);
    }
}

