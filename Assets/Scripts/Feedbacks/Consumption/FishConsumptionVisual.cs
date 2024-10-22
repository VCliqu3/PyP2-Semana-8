using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishConsumptionVisual : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Citizen citizen;
    [SerializeField] private Transform feedbackPrefab;

    [Header("Settings")]
    [SerializeField] Sprite fishSprite;
    [SerializeField] private Vector3 offset;

    private void OnEnable()
    {
        citizen.OnFishConsumption += Citizen_OnFishConsumption;
    }

    private void OnDisable()
    {
        citizen.OnFishConsumption -= Citizen_OnFishConsumption;
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

        feedbackUI.SetFeedbackImage(fishSprite);
        feedbackUI.SetFeedbackText($"-{quantity}");
    }

    private void Citizen_OnFishConsumption(object sender, Citizen.OnCitizenConsumptionEventArgs e)
    {
        ShowFeedback(e.quantity);
    }
}
