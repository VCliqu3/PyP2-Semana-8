using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeedbackUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject parent;
    [SerializeField] private Image feedbackImage;
    [SerializeField] private TextMeshProUGUI feedbackText;

    [Header("Settings")]
    [SerializeField] private float lifetime;


    private void Start()
    {
        Destroy(parent, lifetime);
    }

    public void SetFeedbackImage(Sprite sprite) => feedbackImage.sprite = sprite;
    public void SetFeedbackText(string text) => feedbackText.text = text;
}
