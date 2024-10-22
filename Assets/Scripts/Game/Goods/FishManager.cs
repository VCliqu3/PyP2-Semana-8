using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int fish;

    public int Fish => fish;

    public static event EventHandler<OnFishEventArgs> OnFishInitialized;
    public static event EventHandler<OnFishEventArgs> OnFishIncreased;
    public static event EventHandler<OnFishEventArgs> OnFishDecreased;
    public static event EventHandler<OnFishEventArgs> OnFishReachZero;

    public class OnFishEventArgs : EventArgs
    {
        public int quantity;
        public int fish;
    }

    private void OnEnable()
    {
        FishHandler.OnAnyFishCollected += FishHandler_OnAnyFishCollected;
        Citizen.OnAnyFishConsumption += Citizen_OnAnyFishConsumption;
    }

    private void OnDisable()
    {
        FishHandler.OnAnyFishCollected -= FishHandler_OnAnyFishCollected;
        Citizen.OnAnyFishConsumption -= Citizen_OnAnyFishConsumption;
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
            Debug.LogWarning("There is more than one FishManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeVariables()
    {
        fish = GameManager.Instance.GameSettings.startingFish;
        OnFishInitialized?.Invoke(this, new OnFishEventArgs { quantity = 0, fish = fish });   
    }

    public void AddFish(int quantity)
    {
        fish += quantity;
        OnFishIncreased?.Invoke(this, new OnFishEventArgs { quantity = quantity, fish = fish });
    }

    public void ReduceFish(int quantity)
    {
        fish = fish - quantity < 0 ? 0 : fish - quantity;
        OnFishDecreased?.Invoke(this, new OnFishEventArgs { quantity = quantity, fish = fish });

        if (fish <= 0) OnFishReachZero?.Invoke(this, new OnFishEventArgs { quantity = 0, fish = fish });
    }

    #region FishHandler Subscriptions
    private void FishHandler_OnAnyFishCollected(object sender, FishHandler.OnFishEventArgs e)
    {
        AddFish(GameManager.Instance.GameSettings.fishQuantityPerFish);
    }
    #endregion

    #region Citizen Subscriptions
    private void Citizen_OnAnyFishConsumption(object sender, Citizen.OnAnyCitizenConsumptionEventArgs e)
    {
        ReduceFish(e.quantity);
    }
    #endregion
}
