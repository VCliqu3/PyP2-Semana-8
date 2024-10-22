using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MeatManager;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MineralsManager : MonoBehaviour
{
    public static MineralsManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int minerals;

    public int Minerals => minerals;

    public static event EventHandler<OnMineralsEventArgs> OnMineralsInitialized;
    public static event EventHandler<OnMineralsEventArgs> OnMineralsIncreased;
    public static event EventHandler<OnMineralsEventArgs> OnMineralsDecreased;
    public static event EventHandler<OnMineralsEventArgs> OnMineralsReachZero;

    public class OnMineralsEventArgs : EventArgs
    {
        public int minerals;
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
            Debug.LogWarning("There is more than one MineralsManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeVariables()
    {
        minerals = GameManager.Instance.GameSettings.startingMinerals;
        OnMineralsInitialized?.Invoke(this, new OnMineralsEventArgs { minerals = minerals });
    }

    public void AddMinerals(int quantity)
    {
        minerals += quantity;
        OnMineralsIncreased?.Invoke(this, new OnMineralsEventArgs { minerals = minerals });
    }

    public void ReduceMinerals(int quantity)
    {
        minerals = minerals - quantity < 0 ? 0 : minerals - quantity;
        OnMineralsDecreased?.Invoke(this, new OnMineralsEventArgs { minerals = minerals });

        if (minerals <= 0) OnMineralsReachZero?.Invoke(this, new OnMineralsEventArgs { minerals = minerals });
    }
}
