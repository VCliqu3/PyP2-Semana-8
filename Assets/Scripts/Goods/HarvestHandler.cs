using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HarvestHandler : MonoBehaviour, IInteractable
{
    public event EventHandler OnObjectSelected;
    public event EventHandler OnObjectDeselected;
    public event EventHandler OnObjectInteracted;

    public static event EventHandler<OnHarvestEventArgs> OnHarvestCollected;

    public class OnHarvestEventArgs : EventArgs
    {
        public HarvestHandler harvest;
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

        CollectHarvest();
    }

    public Transform GetTransform() => transform;

    private void CollectHarvest()
    {
        OnHarvestCollected?.Invoke(this, new OnHarvestEventArgs { harvest = this });
        Destroy(gameObject);
    }
}