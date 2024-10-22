using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatManager : MonoBehaviour
{
    public static MeatManager Instance {  get; private set; }

    [Header("Settings")]
    [SerializeField] private int meat;
    [SerializeField] private GameSettings gameSettingsSO;

    public int Meat => meat;

    public static event EventHandler<OnMeatEventArgs> OnMeatIncreased;
    public static event EventHandler<OnMeatEventArgs> OnMeatDecreased;
    public static event EventHandler<OnMeatEventArgs> OnMeatReachZero;

    public class OnMeatEventArgs : EventArgs
    {
        public int meat;
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
            Debug.LogWarning("There is more than one MeatManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeVariables()
    {
        meat = gameSettingsSO.startingMeat;
    }

    public void AddMeat(int quantity) 
    {
        meat += quantity;
        OnMeatIncreased?.Invoke(this, new OnMeatEventArgs { meat = meat });
    }

    public void ReduceMeat(int quantity)
    {
        meat = meat -quantity <0 ? 0 : meat -quantity;
        OnMeatDecreased?.Invoke(this, new OnMeatEventArgs { meat = meat });

        if (meat<=0) OnMeatReachZero?.Invoke(this, new OnMeatEventArgs { meat = meat });
    }
}
