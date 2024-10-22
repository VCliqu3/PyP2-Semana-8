using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishHandler : MonoBehaviour, IInteractable
{
    public event EventHandler OnObjectSelected;
    public event EventHandler OnObjectDeselected;
    public event EventHandler OnObjectInteracted;

    public static event EventHandler<OnFishEventArgs> OnAnyFishCollected;
    public event EventHandler OnFishCollected;

    public class OnFishEventArgs : EventArgs
    {
        public FishHandler fish;
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

        CollectFish();
    }

    public Transform GetTransform() => transform;

    private void CollectFish()
    {
        OnFishCollected?.Invoke(this, EventArgs.Empty);
        OnAnyFishCollected?.Invoke(this, new OnFishEventArgs { fish = this });
        Destroy(gameObject);
    }
}