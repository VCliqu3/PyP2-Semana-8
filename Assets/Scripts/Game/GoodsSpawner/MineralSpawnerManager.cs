using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralSpawnerManager : MonoBehaviour
{
    public static MineralSpawnerManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private Transform mineralPrefab;
    [SerializeField] private List<MineralSpawnPosition> mineralSpawnPositions;

    private float timer = 0f;

    public static event EventHandler OnMineralNotSpawned;
    public static event EventHandler<OnMineralSpawnEventArgs> OnMineralSpawned;
    public static event EventHandler<OnMineralSpawnEventArgs> OnMineralDespawned;

    [Serializable]
    public class MineralSpawnPosition
    {
        public Transform mineralPosition;
        public Transform mineralTransform;
    }

    public class OnMineralSpawnEventArgs : EventArgs
    {
        public Transform mineralTransform;
    }

    private void OnEnable()
    {
        MineralHandler.OnMineralCollected += MineralHandler_OnMineralCollected;
    }

    private void OnDisable()
    {
        MineralHandler.OnMineralCollected -= MineralHandler_OnMineralCollected;
    }

    private void Awake()
    {
        SetSingleton();
    }


    private void Update()
    {
        HandleMineralSpawn();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one MineralSpawnerManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void HandleMineralSpawn()
    {
        if (TimerOnCooldown())
        {
            timer += Time.deltaTime;
            return;
        }

        SpawnMineral();
        ResetTimer();
    }


    public bool SpawnMineral()
    {
        List<MineralSpawnPosition> availablePositions = GetAvailableSpawnPositions();

        if (availablePositions.Count <= 0)
        {
            OnMineralNotSpawned?.Invoke(this, EventArgs.Empty);
            return false;
        }

        MineralSpawnPosition chosenSpawnPosition = ChooseRandomMineralSpawnPosition(availablePositions);

        Transform mineralTransform = Instantiate(mineralPrefab, chosenSpawnPosition.mineralPosition);
        mineralTransform.localPosition = Vector3.zero;

        chosenSpawnPosition.mineralTransform = mineralTransform;

        OnMineralSpawned?.Invoke(this, new OnMineralSpawnEventArgs { mineralTransform = mineralTransform });
        return true;
    }

    private MineralSpawnPosition ChooseRandomMineralSpawnPosition(List<MineralSpawnPosition> mineralSpawnPositions)
    {
        int randomIndex = UnityEngine.Random.Range(0, mineralSpawnPositions.Count);
        return mineralSpawnPositions[randomIndex];
    }

    public bool DespawnMineral(Transform mineralTransform)
    {
        foreach (MineralSpawnPosition mineralSpawnPosition in mineralSpawnPositions)
        {
            if (mineralSpawnPosition.mineralTransform == mineralTransform)
            {
                mineralSpawnPosition.mineralTransform = null;

                OnMineralDespawned?.Invoke(this, new OnMineralSpawnEventArgs { mineralTransform = mineralTransform });
                return true;
            }
        }

        return false;
    }

    private List<MineralSpawnPosition> GetAvailableSpawnPositions()
    {
        List<MineralSpawnPosition> availablePositions = new List<MineralSpawnPosition>();

        foreach (MineralSpawnPosition mineralSpawnPosition in mineralSpawnPositions)
        {
            if (mineralSpawnPosition.mineralTransform != null) continue;
            availablePositions.Add(mineralSpawnPosition);
        }

        return availablePositions;
    }

    private float CalculateTimeToSpawn()
    {
        float spawnRate = CitizensManager.Instance.CitizenCount * GameManager.Instance.GameSettings.mineralsProductionPerSecondPerCitizen;
        return 1 / spawnRate;
    }

    private void ResetTimer() => timer = 0f;
    private bool TimerOnCooldown() => timer < CalculateTimeToSpawn();

    #region MineralHandler Subscriptions
    private void MineralHandler_OnMineralCollected(object sender, MineralHandler.OnMineralEventArgs e)
    {
        DespawnMineral(e.mineral.GetTransform());
    }
    #endregion
}
