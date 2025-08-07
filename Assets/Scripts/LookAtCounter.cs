using UnityEngine;

public class LookAtCounter : MonoBehaviour
{
    //enums are a fixed set of options you can have of something
    private enum Mode
    {
        LookAt, 
        LookAtInverted, 
        CameraForward,
        CameraForwardInverted, 

    }

    [SerializeField] private Mode mode; 

    //is going to run after the regular update 
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                //this function makes a transform look at another transform 
                transform.LookAt(Camera.main.transform.position);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCmera = transform.position -  Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCmera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward; 
                break;

        }
    }
}
