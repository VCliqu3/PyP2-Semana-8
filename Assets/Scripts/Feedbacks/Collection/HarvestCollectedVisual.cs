using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestCollectedVisual : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private HarvestHandler harvestHandler;
    [SerializeField] private Transform feedbackPrefab;

    [Header("Settings")]
    [SerializeField] Sprite harvestSprite;
    [SerializeField] private Vector3 offset;

    private void OnEnable()
    {
        harvestHandler.OnHarvestCollected += HarvestHandler_OnHarvestCollected;
    }

    private void OnDisable()
    {
        harvestHandler.OnHarvestCollected -= HarvestHandler_OnHarvestCollected;
    }

    private void ShowFeedback()
    {
        int quantity = GameManager.Instance.GameSettings.harvestQuantityPerHarvest;

        Transform feedbackTransform = Instantiate(feedbackPrefab, transform.position + offset, transform.rotation);

        FeedbackUI feedbackUI = feedbackTransform.GetComponentInChildren<FeedbackUI>();

        if (!feedbackUI)
        {
            Debug.LogWarning("There's not a FeedbackUI attached to instantiated prefab");
            return;
        }

        feedbackUI.SetFeedbackImage(harvestSprite);
        feedbackUI.SetFeedbackText($"+{quantity}");
    }

    private void HarvestHandler_OnHarvestCollected(object sender, EventArgs e)
    {
        ShowFeedback();
    }
}
