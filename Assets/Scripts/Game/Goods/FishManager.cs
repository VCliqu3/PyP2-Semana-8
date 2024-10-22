using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int fish;
    [SerializeField] private GameSettings gameSettingsSO;

    public int Fish => fish;

    public static event EventHandler<OnFishEventArgs> OnFishReachZero;

    public class OnFishEventArgs : EventArgs
    {
        public int fish;
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
            Debug.LogWarning("There is more than one FishManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeVariables()
    {
        fish = gameSettingsSO.startingFish;
    }

    public void AddFish(int quantity) => fish += quantity;

    public void ReduceFish(int quantity)
    {
        fish = fish - quantity < 0 ? 0 : fish - quantity;

        if (fish <= 0) OnFishReachZero?.Invoke(this, new OnFishEventArgs { fish = fish });

    }
}
