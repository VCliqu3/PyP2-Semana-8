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
    [SerializeField] private GameSettings gameSettingsSO;

    public int Harvests => harvests;

    public static event EventHandler<OnHarvestsEventArgs> OnHarvestIncreased;
    public static event EventHandler<OnHarvestsEventArgs> OnHarvestsDecreased;
    public static event EventHandler<OnHarvestsEventArgs> OnHarvestsReachZero;

    public class OnHarvestsEventArgs : EventArgs
    {
        public int harvests;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

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
        harvests = gameSettingsSO.startingHarvests;
    }

    public void AddHarvests(int quantity)
    {
        harvests += quantity;
        OnHarvestIncreased?.Invoke(this, new OnHarvestsEventArgs { harvests = harvests });
    }

    public void ReduceHarvests(int quantity)
    {
        harvests = harvests - quantity < 0 ? 0 : harvests - quantity;

        OnHarvestsDecreased?.Invoke(this, new OnHarvestsEventArgs { harvests = harvests });

        if (harvests <= 0) OnHarvestsReachZero?.Invoke(this, new OnHarvestsEventArgs { harvests = harvests });
    }
}
