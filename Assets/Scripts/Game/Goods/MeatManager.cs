using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatManager : MonoBehaviour
{
    public static MeatManager Instance {  get; private set; }

    [Header("Settings")]
    [SerializeField] private int meat;

    public int Meat => meat;

    public static event EventHandler<OnMeatEventArgs> OnMeatInitialized;
    public static event EventHandler<OnMeatEventArgs> OnMeatIncreased;
    public static event EventHandler<OnMeatEventArgs> OnMeatDecreased;
    public static event EventHandler<OnMeatEventArgs> OnMeatReachZero;

    public class OnMeatEventArgs : EventArgs
    {
        public int quantity;
        public int meat;
    }

    private void OnEnable()
    {
        MeatHandler.OnMeatCollected += MeatHandler_OnMeatCollected;
        Citizen.OnAnyMeatConsumption += Citizen_OnAnyMeatConsumption;
    }

    private void OnDisable()
    {
        MeatHandler.OnMeatCollected -= MeatHandler_OnMeatCollected;
        Citizen.OnAnyMeatConsumption -= Citizen_OnAnyMeatConsumption;
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
            Debug.LogWarning("There is more than one MeatManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeVariables()
    {
        meat = GameManager.Instance.GameSettings.startingMeat;
        OnMeatInitialized?.Invoke(this , new OnMeatEventArgs { quantity = 0, meat = meat});
    }

    public void AddMeat(int quantity) 
    {
        meat += quantity;
        OnMeatIncreased?.Invoke(this, new OnMeatEventArgs { quantity = quantity, meat = meat });
    }

    public void ReduceMeat(int quantity)
    {
        meat = meat -quantity <0 ? 0 : meat -quantity;
        OnMeatDecreased?.Invoke(this, new OnMeatEventArgs { quantity = quantity, meat = meat });

        if (meat<=0) OnMeatReachZero?.Invoke(this, new OnMeatEventArgs { quantity = 0, meat = meat });
    }

    #region MeatHandler Subscriptions
    private void MeatHandler_OnMeatCollected(object sender, MeatHandler.OnMeatEventArgs e)
    {
        AddMeat(GameManager.Instance.GameSettings.meatQuantityPerMeat);
    }

    #endregion

    #region CitizenSubscriptions
    private void Citizen_OnAnyMeatConsumption(object sender, Citizen.OnAnyCitizenConsumptionEventArgs e)
    {
        ReduceMeat(e.quantity);
    }
    #endregion
}
