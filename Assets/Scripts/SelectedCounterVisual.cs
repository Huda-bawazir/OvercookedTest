using UnityEditor.Search;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private MeshRenderer meshRenderer;

    //instead of using a seralized field the way we did for gameinput in the Player's script.
    //we can use singlton pattern. 
    private void Start()
    {
        //in start so that it runs after the script where the instance runs. 
        Player.Instance.OnSelectedcounterChange += Player_OnSelectedCounterChange;
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        //compare which counter this visual belongs to. 
        if (e.selectedCounter == clearCounter)
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
        meshRenderer.material = selectedMaterial;
    }

    private void Hide()
    {
        meshRenderer.material = normalMaterial;
    }
}

