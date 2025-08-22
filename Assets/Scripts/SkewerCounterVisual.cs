using System.Collections.Generic;
using UnityEngine;

public class SkewerCounterVisual : MonoBehaviour
{
    //where to spwan the isual so add a refrence to a counter top point.
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform SkewerVisualPrefab;
    public float plateOffsetY = .1f;
    public Vector3 Offset;

    private List<GameObject> SkewerVisualGameObjectList;

    private void Awake()
    {
        SkewerVisualGameObjectList = new List<GameObject>();

    }

    //warning this isnt clean code. This was taken from the plate counter to emulate behaviour. as it is the variant of the plate counter

    private void Start()
    {
        platesCounter.OnPlateSpawned += SkewerCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += SkewerCounter_OnPlateRemoved;
    }

    private void SkewerCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = SkewerVisualGameObjectList[SkewerVisualGameObjectList.Count - 1];
        SkewerVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void SkewerCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        //spwaning the prefab 
        Transform skewerVisualTransform = Instantiate(SkewerVisualPrefab, counterTopPoint);

        skewerVisualTransform.localPosition = new Vector3(plateOffsetY * SkewerVisualGameObjectList.Count, 0, 0) + Offset;
        
        SkewerVisualGameObjectList.Add(skewerVisualTransform.gameObject);
    }
}
