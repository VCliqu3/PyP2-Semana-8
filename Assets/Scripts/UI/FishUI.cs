using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI fishQuantityText;

    private void OnEnable()
    {
        FishManager.OnFishInitialized += FishManager_OnFishInitialized;
        FishManager.OnFishIncreased += FishManager_OnFishIncreased;
        FishManager.OnFishDecreased += FishManager_OnFishDecreased;
    }

    private void OnDisable()
    {
        FishManager.OnFishInitialized -= FishManager_OnFishInitialized;
        FishManager.OnFishIncreased -= FishManager_OnFishIncreased;
        FishManager.OnFishDecreased -= FishManager_OnFishDecreased;
    }

    private void UpdateFishQuantityText(int quantity) => fishQuantityText.text = quantity.ToString();

    #region FishManager Subscriptions
    private void FishManager_OnFishIncreased(object sender, FishManager.OnFishEventArgs e)
    {
        UpdateFishQuantityText(e.fish);
    }

    private void FishManager_OnFishInitialized(object sender, FishManager.OnFishEventArgs e)
    {
        UpdateFishQuantityText(e.fish);
    }

    private void FishManager_OnFishDecreased(object sender, FishManager.OnFishEventArgs e)
    {
        UpdateFishQuantityText(e.fish);
    }
    #endregion
}
