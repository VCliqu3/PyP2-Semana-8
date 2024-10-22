using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mineral : MonoBehaviour, IInteractable
{
    public event EventHandler OnObjectSelected;
    public event EventHandler OnObjectDeselected;
    public event EventHandler OnObjectInteracted;

    public void Select()
    {
        Debug.Log("Select");
        OnObjectSelected?.Invoke(this, EventArgs.Empty);
    }

    public void Deselect()
    {
        Debug.Log("Deselect");
        OnObjectDeselected?.Invoke(this, EventArgs.Empty);
    }

    public void Interact()
    {
        Debug.Log("Interact");
        OnObjectInteracted?.Invoke(this, EventArgs.Empty);
    }

    public Transform GetTransform() => transform;
}
