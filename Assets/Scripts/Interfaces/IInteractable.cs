using UnityEngine;
using System;

public interface IInteractable
{
    public event EventHandler OnObjectSelected;
    public event EventHandler OnObjectDeselected;

    public event EventHandler OnObjectInteracted;

    public void Select();
    public void Deselect();
    public void Interact();
    public Transform GetTransform();
}