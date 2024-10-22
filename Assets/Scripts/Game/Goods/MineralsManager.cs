using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralsManager : MonoBehaviour
{
    public static MineralsManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int minerals;
    [SerializeField] private GameSettings gameSettingsSO;

    public int Minerals => minerals;

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
        minerals = gameSettingsSO.startingMinerals;
    }

    public void AddMinerals(int quantity) => minerals += quantity;

    public void ReduceMinerals(int quantity)
    {
        minerals = minerals - quantity < 0 ? 0 : minerals - quantity;

        if (minerals <= 0) OnMineralsReachZero?.Invoke(this, new OnMineralsEventArgs { minerals = minerals });
    }
}
