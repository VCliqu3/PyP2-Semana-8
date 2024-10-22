using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MineralUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI mineralQuantityText;

    private void OnEnable()
    {
        MineralsManager.OnMineralsInitialized += MineralsManager_OnMineralsInitialized;
        MineralsManager.OnMineralsIncreased += MineralsManager_OnMineralsIncreased;
        MineralsManager.OnMineralsDecreased += MineralsManager_OnMineralsDecreased;
    }

    private void OnDisable()
    {
        MineralsManager.OnMineralsInitialized -= MineralsManager_OnMineralsInitialized;
        MineralsManager.OnMineralsIncreased -= MineralsManager_OnMineralsIncreased;
        MineralsManager.OnMineralsDecreased -= MineralsManager_OnMineralsDecreased;
    }

    private void UpdateMineralsQuantityText(int quantity) => mineralQuantityText.text = quantity.ToString();

    #region MineralsManager Subscriptions
    private void MineralsManager_OnMineralsIncreased(object sender, MineralsManager.OnMineralsEventArgs e)
    {
        UpdateMineralsQuantityText(e.minerals);
    }

    private void MineralsManager_OnMineralsInitialized(object sender, MineralsManager.OnMineralsEventArgs e)
    {
        UpdateMineralsQuantityText(e.minerals);
    }

    private void MineralsManager_OnMineralsDecreased(object sender, MineralsManager.OnMineralsEventArgs e)
    {
        UpdateMineralsQuantityText(e.minerals);
    }
    #endregion
}
