using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MineralsManager;

public class HarvestsManager : MonoBehaviour
{
    public static HarvestsManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int harvests;

    public int Harvests => harvests;

    public static event EventHandler<OnHarvestsEventArgs> OnHarvestsInitialized;
    public static event EventHandler<OnHarvestsEventArgs> OnHarvestsIncreased;
    public static event EventHandler<OnHarvestsEventArgs> OnHarvestsDecreased;
    public static event EventHandler<OnHarvestsEventArgs> OnHarvestsReachZero;

    public class OnHarvestsEventArgs : EventArgs
    {
        public int quantity;
        public int harvests;
    }

    private void OnEnable()
    {
        HarvestHandler.OnAnyHarvestCollected += HarvestHandler_OnAnyHarvestCollected;
        Citizen.OnAnyHarvestConsumption += Citizen_OnAnyHarvestConsumption;
    }

    private void OnDisable()
    {
        HarvestHandler.OnAnyHarvestCollected -= HarvestHandler_OnAnyHarvestCollected;
        Citizen.OnAnyHarvestConsumption -= Citizen_OnAnyHarvestConsumption;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one HarvestsManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeVariables()
    {
        harvests = GameManager.Instance.GameSettings.startingHarvests;
        OnHarvestsInitialized?.Invoke(this, new OnHarvestsEventArgs { quantity = 0, harvests = harvests });   
    }

    public void AddHarvests(int quantity)
    {
        harvests += quantity;
        OnHarvestsIncreased?.Invoke(this, new OnHarvestsEventArgs { quantity = quantity, harvests = harvests });
    }

    public void ReduceHarvests(int quantity)
    {
        harvests = harvests - quantity < 0 ? 0 : harvests - quantity;

        OnHarvestsDecreased?.Invoke(this, new OnHarvestsEventArgs { quantity = quantity, harvests = harvests });

        if (harvests <= 0) OnHarvestsReachZero?.Invoke(this, new OnHarvestsEventArgs { quantity = 0, harvests = harvests });
    }

    #region HarvestHandler Subcriptions
    private void HarvestHandler_OnAnyHarvestCollected(object sender, HarvestHandler.OnHarvestEventArgs e)
    {
        AddHarvests(GameManager.Instance.GameSettings.harvestQuantityPerHarvest);
    }
    #endregion

    #region Citizen Subscriptions
    private void Citizen_OnAnyHarvestConsumption(object sender, Citizen.OnAnyCitizenConsumptionEventArgs e)
    {
        ReduceHarvests(e.quantity);
    }
    #endregion
}
