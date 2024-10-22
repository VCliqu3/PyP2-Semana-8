using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeatUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI meatQuantityText;

    private void OnEnable()
    {
        MeatManager.OnMeatInitialized += MeatManager_OnMeatInitialized;
        MeatManager.OnMeatIncreased += MeatManager_OnMeatIncreased;
        MeatManager.OnMeatDecreased += MeatManager_OnMeatDecreased;
    }

    private void OnDisable()
    {
        MeatManager.OnMeatInitialized -= MeatManager_OnMeatInitialized;
        MeatManager.OnMeatIncreased -= MeatManager_OnMeatIncreased;
        MeatManager.OnMeatDecreased -= MeatManager_OnMeatDecreased;
    }

    private void UpdateMeatQuantityText(int quantity) => meatQuantityText.text = quantity.ToString();

    #region MeatManager Subscriptions
    private void MeatManager_OnMeatIncreased(object sender, MeatManager.OnMeatEventArgs e)
    {
        UpdateMeatQuantityText(e.meat);
    }

    private void MeatManager_OnMeatInitialized(object sender, MeatManager.OnMeatEventArgs e)
    {
        UpdateMeatQuantityText(e.meat);
    }

    private void MeatManager_OnMeatDecreased(object sender, MeatManager.OnMeatEventArgs e)
    {
        UpdateMeatQuantityText(e.meat);
    }
    #endregion
}
