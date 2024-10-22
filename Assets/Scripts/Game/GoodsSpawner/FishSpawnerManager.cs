using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HarvestSpawnerManager;

public class FishSpawnerManager : MonoBehaviour
{
    public static FishSpawnerManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private Transform fishPrefab;
    [SerializeField] private List<FishSpawnPosition> fishSpawnPositions;

    private float timer = 0f;

    public static event EventHandler OnFishNotSpawned;
    public static event EventHandler<OnFishSpawnEventArgs> OnFishSpawned;
    public static event EventHandler<OnFishSpawnEventArgs> OnFishDespawned;

    [Serializable]
    public class FishSpawnPosition
    {
        public Transform fishPosition;
        public Transform fishTransform;
    }

    public class OnFishSpawnEventArgs : EventArgs
    {
        public Transform fishTransform;
    }

    private void OnEnable()
    {
        FishHandler.OnFishCollected += FishHandler_OnFishCollected;
    }

    private void OnDisable()
    {
        FishHandler.OnFishCollected -= FishHandler_OnFishCollected;
    }
    private void Awake()
    {
        SetSingleton();
    }


    private void Update()
    {
        HandleFishSpawn();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one FishSpawnerManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void HandleFishSpawn()
    {
        if (TimerOnCooldown())
        {
            timer += Time.deltaTime;
            return;
        }

        SpawnFish();
        ResetTimer();
    }


    public bool SpawnFish()
    {
        List<FishSpawnPosition> availablePositions = GetAvailableSpawnPositions();

        if (availablePositions.Count <= 0)
        {
            OnFishNotSpawned?.Invoke(this, EventArgs.Empty);
            return false;
        }

        FishSpawnPosition chosenSpawnPosition = ChooseRandomFishSpawnPosition(availablePositions);

        Transform fishTransform = Instantiate(fishPrefab, chosenSpawnPosition.fishPosition);
        fishTransform.localPosition = Vector3.zero;

        chosenSpawnPosition.fishTransform = fishTransform;

        OnFishSpawned?.Invoke(this, new OnFishSpawnEventArgs { fishTransform = fishTransform });
        return true;
    }

    private FishSpawnPosition ChooseRandomFishSpawnPosition(List<FishSpawnPosition> fishSpawnPositions)
    {
        int randomIndex = UnityEngine.Random.Range(0, fishSpawnPositions.Count);
        return fishSpawnPositions[randomIndex];
    }

    public bool DespawnFish(Transform fishTransform)
    {
        foreach (FishSpawnPosition fishSpawnPosition in fishSpawnPositions)
        {
            if (fishSpawnPosition.fishTransform == fishTransform)
            {
                fishSpawnPosition.fishTransform = null;

                OnFishDespawned?.Invoke(this, new OnFishSpawnEventArgs { fishTransform = fishTransform });
                return true;
            }
        }

        return false;
    }

    private List<FishSpawnPosition> GetAvailableSpawnPositions()
    {
        List<FishSpawnPosition> availablePositions = new List<FishSpawnPosition>();

        foreach (FishSpawnPosition fishSpawnPosition in fishSpawnPositions)
        {
            if (fishSpawnPosition.fishTransform != null) continue;
            availablePositions.Add(fishSpawnPosition);
        }

        return availablePositions;
    }

    private float CalculateTimeToSpawn()
    {
        float spawnRate = CitizensManager.Instance.CitizenCount * GameManager.Instance.GameSettings.fishProductionPerSecondPerCitizen;
        return 1 / spawnRate;
    }

    private void ResetTimer() => timer = 0f;
    private bool TimerOnCooldown() => timer < CalculateTimeToSpawn();

    #region FishHandler Subscriptions
    private void FishHandler_OnFishCollected(object sender, FishHandler.OnFishEventArgs e)
    {
        DespawnFish(e.fish.GetTransform());
    }

    #endregion
}
