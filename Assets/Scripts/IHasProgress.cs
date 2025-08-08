using System;
using UnityEngine;

public interface IHasProgress 
{
    //need somekind o event to update bar imagie 
    public event EventHandler<OnProgressChangedEventsArgs> OnProgressChanged;
    public class OnProgressChangedEventsArgs : EventArgs
    {
        public float progressNormalized;
    }

}
