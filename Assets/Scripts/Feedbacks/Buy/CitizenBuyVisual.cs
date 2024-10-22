using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenBuyVisual : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Citizen citizen;
    [SerializeField] private Transform feedbackPrefab;

    [Header("Settings")]
    [SerializeField] Sprite mineralSprite;
    [SerializeField] private Vector3 offset;

    private void OnEnable()
    {
        citizen.OnCitizenBought += Citizen_OnCitizenBought;
    }

    private void OnDisable()
    {
        citizen.OnCitizenBought -= Citizen_OnCitizenBought;
    }

    private void ShowFeedback()
    {
        int price = GameManager.Instance.GameSettings.citizenMineralPrice;

        Transform feedbackTransform = Instantiate(feedbackPrefab, transform.position + offset, transform.rotation);

        FeedbackUI feedbackUI = feedbackTransform.GetComponentInChildren<FeedbackUI>();

        if (!feedbackUI)
        {
            Debug.LogWarning("There's not a FeedbackUI attached to instantiated prefab");
            return;
        }

        feedbackUI.SetFeedbackImage(mineralSprite);
        feedbackUI.SetFeedbackText($"-{price}");
    }

    private void Citizen_OnCitizenBought(object sender, EventArgs e)
    {
        ShowFeedback();
    }
}
