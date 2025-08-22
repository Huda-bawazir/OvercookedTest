using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class GrillCounterVisuals : MonoBehaviour
{
    [SerializeField] private GrillCounter GrillCounter;
    [SerializeField] private GameObject grillOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        GrillCounter.OnStateChanged += GrillCounter_OnStateChanged; ;
    }

    private void GrillCounter_OnStateChanged(object sender, GrillCounter.OnstateChangedEventsArgs e)
    {
        bool showVisual = e.state == GrillCounter.State.Frying || e.state == GrillCounter.State.Fried;
        grillOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);

    }

   
}
