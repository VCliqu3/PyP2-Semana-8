using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollectedVisual : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private FishHandler fishHandler;
    [SerializeField] private Transform feedbackPrefab;

    [Header("Settings")]
    [SerializeField] Sprite fishSprite;
    [SerializeField] private Vector3 offset;

    private void OnEnable()
    {
        fishHandler.OnFishCollected += FishHandler_OnFishCollected;
    }

    private void OnDisable()
    {
        fishHandler.OnFishCollected -= FishHandler_OnFishCollected;
    }

    private void ShowFeedback()
    {
        int quantity = GameManager.Instance.GameSettings.fishQuantityPerFish;

        Transform feedbackTransform = Instantiate(feedbackPrefab, transform.position + offset, transform.rotation);

        FeedbackUI feedbackUI = feedbackTransform.GetComponentInChildren<FeedbackUI>();

        if (!feedbackUI)
        {
            Debug.LogWarning("There's not a FeedbackUI attached to instantiated prefab");
            return;
        }

        feedbackUI.SetFeedbackImage(fishSprite);
        feedbackUI.SetFeedbackText($"+{quantity}");
    }

    private void FishHandler_OnFishCollected(object sender, EventArgs e)
    {
        ShowFeedback();
    }
}
