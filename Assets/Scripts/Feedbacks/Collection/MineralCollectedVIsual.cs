using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralCollectedVIsual : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MineralHandler mineralHandler;
    [SerializeField] private Transform feedbackPrefab;

    [Header("Settings")]
    [SerializeField] Sprite mineralSprite;
    [SerializeField] private Vector3 offset;

    private void OnEnable()
    {
        mineralHandler.OnMineralCollected += MineralHandler_OnMineralCollected;
    }

    private void OnDisable()
    {
        mineralHandler.OnMineralCollected -= MineralHandler_OnMineralCollected;
    }

    private void ShowFeedback()
    {
        int quantity = GameManager.Instance.GameSettings.mineralQuantityPerMineral;

        Transform feedbackTransform = Instantiate(feedbackPrefab, transform.position + offset, transform.rotation);

        FeedbackUI feedbackUI = feedbackTransform.GetComponentInChildren<FeedbackUI>();

        if (!feedbackUI)
        {
            Debug.LogWarning("There's not a FeedbackUI attached to instantiated prefab");
            return;
        }

        feedbackUI.SetFeedbackImage(mineralSprite);
        feedbackUI.SetFeedbackText($"+{quantity}");
    }

    private void MineralHandler_OnMineralCollected(object sender, EventArgs e)
    {
        ShowFeedback();
    }
}
