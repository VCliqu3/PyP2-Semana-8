using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FishSpawnerManager;

public class MeatSpawnerManager : MonoBehaviour
{
    public static MeatSpawnerManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private Transform meatPrefab;
    [SerializeField] private List<MeatSpawnPosition> meatSpawnPositions;

    [Header("Settings")]
    [SerializeField] private Vector3 spawnOffset;

    private float timer = 0f;

    public static event EventHandler OnMeatNotSpawned;
    public static event EventHandler<OnMeatSpawnEventArgs> OnMeatSpawned;
    public static event EventHandler<OnMeatSpawnEventArgs> OnMeatDespawned;

    [Serializable]
    public class MeatSpawnPosition
    {
        public Transform meatPosition;
        public Transform meatTransform;
    }

    public class OnMeatSpawnEventArgs : EventArgs
    {
        public Transform meatTransform;
    }

    private void OnEnable()
    {
        MeatHandler.OnMeatCollected += MeatHandler_OnMeatCollected;
    }

    private void OnDisable()
    {
        MeatHandler.OnMeatCollected -= MeatHandler_OnMeatCollected;
    }

    private void Awake()
    {
        SetSingleton();
    }


    private void Update()
    {
        HandleMeatSpawn();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one MeatSpawnerManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void HandleMeatSpawn()
    {
        if (TimerOnCooldown())
        {
            timer += Time.deltaTime;
            return;
        }

        SpawnMeat();
        ResetTimer();
    }


    public bool SpawnMeat()
    {
        List<MeatSpawnPosition> availablePositions = GetAvailableSpawnPositions();

        if (availablePositions.Count <= 0)
        {
            OnMeatNotSpawned?.Invoke(this, EventArgs.Empty);
            return false;
        }

        MeatSpawnPosition chosenSpawnPosition = ChooseRandomMeatSpawnPosition(availablePositions);

        Transform meatTransform = Instantiate(meatPrefab, chosenSpawnPosition.meatPosition);
        meatTransform.localPosition = spawnOffset;

        chosenSpawnPosition.meatTransform = meatTransform;

        OnMeatSpawned?.Invoke(this, new OnMeatSpawnEventArgs { meatTransform = meatTransform });
        return true;
    }

    private MeatSpawnPosition ChooseRandomMeatSpawnPosition(List<MeatSpawnPosition> meatSpawnPositions)
    {
        int randomIndex = UnityEngine.Random.Range(0, meatSpawnPositions.Count);
        return meatSpawnPositions[randomIndex];
    }

    public bool DespawnMeat(Transform meatTransform)
    {
        foreach (MeatSpawnPosition meatSpawnPosition in meatSpawnPositions)
        {
            if (meatSpawnPosition.meatTransform == meatTransform)
            {
                meatSpawnPosition.meatTransform = null;

                OnMeatDespawned?.Invoke(this, new OnMeatSpawnEventArgs { meatTransform = meatTransform });
                return true;
            }
        }

        return false;
    }

    private List<MeatSpawnPosition> GetAvailableSpawnPositions()
    {
        List<MeatSpawnPosition> availablePositions = new List<MeatSpawnPosition>();

        foreach (MeatSpawnPosition meatSpawnPosition in meatSpawnPositions)
        {
            if (meatSpawnPosition.meatTransform != null) continue;
            availablePositions.Add(meatSpawnPosition);
        }

        return availablePositions;
    }

    private float CalculateTimeToSpawn()
    {
        float spawnRate = CitizensManager.Instance.CitizenCount * GameManager.Instance.GameSettings.meatProductionPerSecondPerCitizen;
        return 1 / spawnRate;
    }

    private void ResetTimer() => timer = 0f;
    private bool TimerOnCooldown() => timer < CalculateTimeToSpawn();

    #region MeatHandler Subscriptions
    private void MeatHandler_OnMeatCollected(object sender, MeatHandler.OnMeatEventArgs e)
    {
        DespawnMeat(e.meat.GetTransform());
    }
    #endregion
}
