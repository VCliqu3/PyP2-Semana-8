using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MineralUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI mineralQuantityText;
    [SerializeField] private Animator mineralBoxAnimator;

    private const string CANT_AFFORD_TRIGGER = "CantAfford";

    private void OnEnable()
    {
        MineralsManager.OnMineralsInitialized += MineralsManager_OnMineralsInitialized;
        MineralsManager.OnMineralsIncreased += MineralsManager_OnMineralsIncreased;
        MineralsManager.OnMineralsDecreased += MineralsManager_OnMineralsDecreased;

        CitizensManager.OnCantAffordCitizen += CitizensManager_OnCantAffordCitizen;
    }

    private void OnDisable()
    {
        MineralsManager.OnMineralsInitialized -= MineralsManager_OnMineralsInitialized;
        MineralsManager.OnMineralsIncreased -= MineralsManager_OnMineralsIncreased;
        MineralsManager.OnMineralsDecreased -= MineralsManager_OnMineralsDecreased;

        CitizensManager.OnCantAffordCitizen -= CitizensManager_OnCantAffordCitizen;
    }

    private void Start()
    {
        NewMethod();
    }

    private void NewMethod()
    {
        mineralBoxAnimator.ResetTrigger(CANT_AFFORD_TRIGGER);
    }

    private void CantAffordBlink()
    {
        mineralBoxAnimator.SetTrigger(CANT_AFFORD_TRIGGER);
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

    #region CitizenManager Subscriptions
    private void CitizensManager_OnCantAffordCitizen(object sender, System.EventArgs e)
    {
        CantAffordBlink();
    }
    #endregion
}
