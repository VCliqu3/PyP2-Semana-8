using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HarvestUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI harvestQuantityText;

    private void OnEnable()
    {
        HarvestsManager.OnHarvestsInitialized += HarvestsManager_OnHarvestsInitialized;
        HarvestsManager.OnHarvestsIncreased += HarvestsManager_OnHarvestIncreased;
        HarvestsManager.OnHarvestsDecreased += HarvestsManager_OnHarvestsDecreased;
    }

    private void OnDisable()
    {
        HarvestsManager.OnHarvestsInitialized -= HarvestsManager_OnHarvestsInitialized;
        HarvestsManager.OnHarvestsIncreased -= HarvestsManager_OnHarvestIncreased;
        HarvestsManager.OnHarvestsDecreased -= HarvestsManager_OnHarvestsDecreased;
    }

    private void UpdateHarvestQuantityText(int quantity) => harvestQuantityText.text = quantity.ToString();

    #region HarvestManager Subscriptions
    private void HarvestsManager_OnHarvestIncreased(object sender, HarvestsManager.OnHarvestsEventArgs e)
    {
        UpdateHarvestQuantityText(e.harvests);
    }

    private void HarvestsManager_OnHarvestsInitialized(object sender, HarvestsManager.OnHarvestsEventArgs e)
    {
        UpdateHarvestQuantityText(e.harvests);
    }

    private void HarvestsManager_OnHarvestsDecreased(object sender, HarvestsManager.OnHarvestsEventArgs e)
    {
        UpdateHarvestQuantityText(e.harvests);
    }
    #endregion
}
