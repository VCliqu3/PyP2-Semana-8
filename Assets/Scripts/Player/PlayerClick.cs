using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClick : MonoBehaviour
{
    public static PlayerClick Instance { get; private set; }

    [Header("Enabler")]
    [SerializeField] private bool interactionEnabled;

    [Header("Interaction Settings")]
    [SerializeField] private LayerMask interactionLayer;

    [Header("World Space Interaction Settings")]
    [SerializeField, Range(200f, 350f)] private float maxDistanceFromCamera;

    [Header("Debug")]
    [SerializeField] private bool drawRaycasts;

    private IInteractable currentInteractable;
    public IInteractable CurrentInteractable => currentInteractable;

    private void Update()
    {
        HandleInteractableSelections();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        if (currentInteractable == null) return;

        HandleDownInteractions(currentInteractable);
    }

    private void HandleDownInteractions(IInteractable interactable)
    {
        if (InputManager.Instance.GetInteractionInputDown())
        {
            interactable.Interact();
        }

    }

    private void HandleInteractableSelections()
    {
        IInteractable interactable = CheckForInteractableWorldSpace();

        if (interactable != null)
        {
            if (currentInteractable == null)
            {
                SelectInteractable(interactable);
            }
            else if (currentInteractable != interactable)
            {
                DeselectInteractable(currentInteractable);
                SelectInteractable(interactable);
            }
        }
        else if (currentInteractable != null)
        {
            DeselectInteractable(currentInteractable);
        }
    }

    private void SelectInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;

        interactable.Select();
    }

    private void DeselectInteractable(IInteractable interactable)
    {
        currentInteractable = null;

        interactable.Deselect();       
    }


    private IInteractable CheckForInteractableWorldSpace()
    {
        RaycastHit hit = GetInteractableLayerHitsWorldSpace();

        if (hit.collider == null) return null;

        IInteractable potentialInteractable = CheckIfRayHitHasInteractable(hit);

        if (potentialInteractable == null) return null;

        return potentialInteractable;
    }

    private IInteractable CheckIfRayHitHasInteractable(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out IInteractable hitInteractable))
        {
            return hitInteractable;
        }

        return null;
    }

    public RaycastHit GetInteractableLayerHitsWorldSpace()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (drawRaycasts) Debug.DrawRay(ray.origin, ray.direction * maxDistanceFromCamera, Color.red);

        bool hitInteractable = Physics.Raycast(ray, out RaycastHit hit, maxDistanceFromCamera, interactionLayer);

        return hit;
    }
}
