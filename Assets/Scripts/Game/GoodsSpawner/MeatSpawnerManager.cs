using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatSpawnerManager : MonoBehaviour
{
    public static MeatSpawnerManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private Transform meatPrefab;
    [SerializeField] private List<MeatSpawnPosition> meatSpawnPositions;

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

        MeatSpawnPosition chosenSpawnPosition = availablePositions[0];

        Transform meatTransform = Instantiate(meatPrefab, chosenSpawnPosition.meatPosition);
        meatTransform.localPosition = Vector3.zero;

        chosenSpawnPosition.meatTransform = meatTransform;

        OnMeatSpawned?.Invoke(this, new OnMeatSpawnEventArgs { meatTransform = meatTransform });
        return true;
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
}
