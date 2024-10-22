using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCollectedVisual : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MeatHandler meatHandler;
    [SerializeField] private Transform feedbackPrefab;

    [Header("Settings")]
    [SerializeField] Sprite meatSprite;
    [SerializeField] private Vector3 offset;

    private void OnEnable()
    {
        meatHandler.OnMeatCollected += MeatHandler_OnMeatCollected;
    }

    private void OnDisable()
    {
        meatHandler.OnMeatCollected -= MeatHandler_OnMeatCollected;
    }

    private void ShowFeedback()
    {
        int quantity = GameManager.Instance.GameSettings.meatQuantityPerMeat;

        Transform feedbackTransform = Instantiate(feedbackPrefab, transform.position + offset, transform.rotation);

        FeedbackUI feedbackUI = feedbackTransform.GetComponentInChildren<FeedbackUI>();

        if (!feedbackUI)
        {
            Debug.LogWarning("There's not a FeedbackUI attached to instantiated prefab");
            return;
        }

        feedbackUI.SetFeedbackImage(meatSprite);
        feedbackUI.SetFeedbackText($"+{quantity}");
    }

    private void MeatHandler_OnMeatCollected(object sender, EventArgs e)
    {
        ShowFeedback();
    }
}
