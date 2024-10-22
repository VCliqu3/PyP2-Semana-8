using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestSpawnerManager : MonoBehaviour
{
    public static HarvestSpawnerManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private Transform harvestPrefab;
    [SerializeField] private List<HarvestSpawnPosition> harvestSpawnPositions;

    [Header("Settings")]
    [SerializeField] private Vector3 spawnOffset;

    private float timer = 0f;

    public static event EventHandler OnHarvestNotSpawned;
    public static event EventHandler<OnHarvestSpawnEventArgs> OnHarvestSpawned;
    public static event EventHandler<OnHarvestSpawnEventArgs> OnHarvestDespawned;

    [Serializable]
    public class HarvestSpawnPosition
    {
        public Transform harvestPosition;
        public Transform harvestTransform;
    }

    public class OnHarvestSpawnEventArgs : EventArgs
    {
        public Transform harvestTransform;
    }

    private void OnEnable()
    {
        HarvestHandler.OnHarvestCollected += HarvestHandler_OnHarvestCollected;
    }

    private void OnDisable()
    {
        HarvestHandler.OnHarvestCollected -= HarvestHandler_OnHarvestCollected;
    }

    private void Awake()
    {
        SetSingleton();
    }


    private void Update()
    {
        HandleHarvestSpawn();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one HarvestSpawnerManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void HandleHarvestSpawn()
    {
        if (TimerOnCooldown())
        {
            timer += Time.deltaTime;
            return;
        }

        SpawnHarvest();
        ResetTimer();
    }


    public bool SpawnHarvest()
    {
        List<HarvestSpawnPosition> availablePositions = GetAvailableSpawnPositions();

        if (availablePositions.Count <= 0)
        {
            OnHarvestNotSpawned?.Invoke(this, EventArgs.Empty);
            return false;
        }

        HarvestSpawnPosition chosenSpawnPosition = ChooseRandomHarvestSpawnPosition(availablePositions);

        Transform harvestTransform = Instantiate(harvestPrefab, chosenSpawnPosition.harvestPosition);
        harvestTransform.localPosition = spawnOffset;

        chosenSpawnPosition.harvestTransform = harvestTransform;

        OnHarvestSpawned?.Invoke(this, new OnHarvestSpawnEventArgs { harvestTransform = harvestTransform });
        return true;
    }

    private HarvestSpawnPosition ChooseRandomHarvestSpawnPosition(List<HarvestSpawnPosition> harvestSpawnPositions)
    {
        int randomIndex = UnityEngine.Random.Range(0, harvestSpawnPositions.Count);
        return harvestSpawnPositions[randomIndex];
    }

    public bool DespawnHarvest(Transform harvestTransform)
    {
        foreach (HarvestSpawnPosition harvestSpawnPosition in harvestSpawnPositions)
        {
            if (harvestSpawnPosition.harvestTransform == harvestTransform)
            {
                harvestSpawnPosition.harvestTransform = null;

                OnHarvestDespawned?.Invoke(this, new OnHarvestSpawnEventArgs { harvestTransform = harvestTransform });
                return true;
            }
        }

        return false;
    }

    private List<HarvestSpawnPosition> GetAvailableSpawnPositions()
    {
        List<HarvestSpawnPosition> availablePositions = new List<HarvestSpawnPosition>();

        foreach (HarvestSpawnPosition harvestSpawnPosition in harvestSpawnPositions)
        {
            if (harvestSpawnPosition.harvestTransform != null) continue;
            availablePositions.Add(harvestSpawnPosition);
        }

        return availablePositions;
    }

    private float CalculateTimeToSpawn()
    {
        float spawnRate = CitizensManager.Instance.CitizenCount * GameManager.Instance.GameSettings.harvestsProductionPerSecondPerCitizen;
        return 1 / spawnRate;
    }

    private void ResetTimer() => timer = 0f;
    private bool TimerOnCooldown() => timer < CalculateTimeToSpawn();

    #region HarvestHandler Subscriptions
    private void HarvestHandler_OnHarvestCollected(object sender, HarvestHandler.OnHarvestEventArgs e)
    {
        DespawnHarvest(e.harvest.GetTransform());
    }

    #endregion
}
