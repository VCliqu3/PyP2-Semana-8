using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCitizenUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button buyCitizenButton;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        buyCitizenButton.onClick.AddListener(BuyCitizen);
    }

    private void BuyCitizen() => CitizensManager.Instance.BuyCitizen();
}
