using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatConsumptionVisual : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Citizen citizen;
    [SerializeField] private Transform feedbackPrefab;

    [Header("Settings")]
    [SerializeField] Sprite meatSprite;
    [SerializeField] private Vector3 offset;

    private void OnEnable()
    {
        citizen.OnMeatConsumption += Citizen_OnMeatConsumption;
    }

    private void OnDisable()
    {
        citizen.OnMeatConsumption -= Citizen_OnMeatConsumption;
    }

    private void ShowFeedback(int quantity)
    {
        Transform feedbackTransform = Instantiate(feedbackPrefab, transform.position + offset, transform.rotation);

        FeedbackUI feedbackUI = feedbackTransform.GetComponentInChildren<FeedbackUI>();

        if (!feedbackUI)
        {
            Debug.LogWarning("There's not a FeedbackUI attached to instantiated prefab");
            return;
        }

        feedbackUI.SetFeedbackImage(meatSprite);
        feedbackUI.SetFeedbackText($"-{quantity}");
    }

    private void Citizen_OnMeatConsumption(object sender, Citizen.OnCitizenConsumptionEventArgs e)
    {
        ShowFeedback(e.quantity);
    }
}
