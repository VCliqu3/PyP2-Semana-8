using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeatHandler : MonoBehaviour, IInteractable
{
    public event EventHandler OnObjectSelected;
    public event EventHandler OnObjectDeselected;
    public event EventHandler OnObjectInteracted;

    public static event EventHandler<OnMeatEventArgs> OnAnyMeatCollected;

    public event EventHandler OnMeatCollected;

    public class OnMeatEventArgs : EventArgs
    {
        public MeatHandler meat;
    }

    public void Select()
    {
        //Debug.Log("Select");
        OnObjectSelected?.Invoke(this, EventArgs.Empty);
    }

    public void Deselect()
    {
        //Debug.Log("Deselect");
        OnObjectDeselected?.Invoke(this, EventArgs.Empty);
    }

    public void Interact()
    {
        //Debug.Log("Interact");
        OnObjectInteracted?.Invoke(this, EventArgs.Empty);

        CollectMeat();
    }

    public Transform GetTransform() => transform;

    private void CollectMeat()
    {
        OnMeatCollected?.Invoke(this, EventArgs.Empty);
        OnAnyMeatCollected?.Invoke(this, new OnMeatEventArgs { meat = this });
        Destroy(gameObject);
    }
}
