using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MineralHandler : MonoBehaviour, IInteractable
{
    public event EventHandler OnObjectSelected;
    public event EventHandler OnObjectDeselected;
    public event EventHandler OnObjectInteracted;

    public static event EventHandler<OnMineralEventArgs> OnAnyMineralCollected;
    public event EventHandler OnMineralCollected;

    public class OnMineralEventArgs : EventArgs
    {
        public MineralHandler mineral;
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

        CollectMineral();
    }

    public Transform GetTransform() => transform;

    private void CollectMineral()
    {
        OnMineralCollected?.Invoke(this, EventArgs.Empty);
        OnAnyMineralCollected?.Invoke(this, new OnMineralEventArgs { mineral = this });
        Destroy(gameObject);
    }
}
