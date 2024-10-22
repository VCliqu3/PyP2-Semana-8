using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenSpawnerManager : MonoBehaviour
{
    public static CitizenSpawnerManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private Transform citizenPrefab;
    [SerializeField] private List<CitizenSpawnPosition> citizenSpawnPositions;

    public static event EventHandler OnCitizenNotSpawned;
    public static event EventHandler<OnCitizenSpawnEventArgs> OnCitizenSpawned;
    public static event EventHandler<OnCitizenSpawnEventArgs> OnCitizenDespawned;

    [Serializable]
    public class CitizenSpawnPosition
    {
        public Transform citizenPosition;
        public Transform citizenTransform;
    }

    public class OnCitizenSpawnEventArgs : EventArgs
    {
        public Transform citizenTransform;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one CitizensManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public bool SpawnCitizen(bool wasBought)
    {
        List<CitizenSpawnPosition> availablePositions = GetAvailableSpawnPositions();

        if(availablePositions.Count <= 0)
        {
            OnCitizenNotSpawned?.Invoke(this, EventArgs.Empty);
            return false;
        }

        CitizenSpawnPosition chosenSpawnPosition = availablePositions[0];

        Transform citizenTransform = Instantiate(citizenPrefab,chosenSpawnPosition.citizenPosition);
        citizenTransform.localPosition = Vector3.zero;

        chosenSpawnPosition.citizenTransform = citizenTransform;

        OnCitizenSpawned?.Invoke(this, new OnCitizenSpawnEventArgs { citizenTransform = citizenTransform });

        if (wasBought)
        {
            Citizen citizen = citizenTransform.GetComponentInChildren<Citizen>();

            if (citizen == null)
            {
                Debug.Log("Spawned Citizen does not contain a Citizen component");
                return true;
            }

            citizen.SetWasBought();
        }

        return true;
    }

    public bool DespawnCitizen(Transform citizenTransform)
    {
        foreach(CitizenSpawnPosition citizenSpawnPosition in citizenSpawnPositions)
        {
            if(citizenSpawnPosition.citizenTransform == citizenTransform)
            {
                citizenSpawnPosition.citizenTransform = null;

                OnCitizenDespawned?.Invoke(this, new OnCitizenSpawnEventArgs { citizenTransform = citizenTransform });
                return true;
            }
        }

        return false;
    }

    private List<CitizenSpawnPosition> GetAvailableSpawnPositions()
    {
        List<CitizenSpawnPosition> availablePositions = new List<CitizenSpawnPosition>();   

        foreach (CitizenSpawnPosition citizenSpawnPosition in citizenSpawnPositions)
        {
            if (citizenSpawnPosition.citizenTransform != null) continue;
            availablePositions.Add(citizenSpawnPosition);
        }

        return availablePositions;
    }
}
