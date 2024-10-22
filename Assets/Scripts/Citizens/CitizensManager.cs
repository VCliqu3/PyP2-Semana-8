using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CitizensManager : MonoBehaviour
{
    public static CitizensManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private List<Citizen> citizenList;

    public static event EventHandler OnCitizenNotAdded;
    public static event EventHandler<OnCitizenEventArgs> OnCitizenAdded;
    public static event EventHandler<OnCitizenEventArgs> OnCitizenRemoved;

    public static event EventHandler OnCantAffordCitizen;
    public static event EventHandler OnCitizenBought;

    public List<Citizen> CitizenList => citizenList;
    public int CitizenCount => citizenList.Count;

    public class OnCitizenEventArgs : EventArgs
    {
        public Citizen citizen;
    }

    private void OnEnable()
    {
        CitizenSpawnerManager.OnCitizenSpawned += CitizenSpawnerManager_OnCitizenSpawned;
        CitizenSpawnerManager.OnCitizenDespawned += CitizenSpawnerManager_OnCitizenDespawned;
    }

    private void OnDisable()
    {
        CitizenSpawnerManager.OnCitizenSpawned -= CitizenSpawnerManager_OnCitizenSpawned;
        CitizenSpawnerManager.OnCitizenDespawned -= CitizenSpawnerManager_OnCitizenDespawned;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        InitializeCitizens();
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

    private void InitializeCitizens()
    {
        int startingCitizensCount = GameManager.Instance.GameSettings.startingCitizens;
        
        for (int i = 0; i < startingCitizensCount; i++)
        {
            AddCitizen();
        }
    }

    public void BuyCitizen()
    {

        if (!CanAffordCitizen())
        {
        Debug.Log("Test");
            OnCantAffordCitizen?.Invoke(this, EventArgs.Empty);
            return;
        }

        bool addedCitizen = AddCitizen();

        if (!addedCitizen) return;

        OnCitizenBought?.Invoke(this, EventArgs.Empty);
    }

    private bool CanAffordCitizen() => MineralsManager.Instance.Minerals >= GameManager.Instance.GameSettings.citizenMineralPrice ;

    private bool AddCitizen()
    {
        bool added = CitizenSpawnerManager.Instance.SpawnCitizen();

        if (!added) OnCitizenNotAdded?.Invoke(this, EventArgs.Empty);

        return added;
    }

    private void RemoveCitizen(Citizen citizen)=> CitizenSpawnerManager.Instance.DespawnCitizen(citizen.transform);

    private void AddCitizenToList(Transform citizenTransform)
    {
        Citizen citizen = citizenTransform.GetComponent<Citizen>();

        if (citizen == null) return;

        citizenList.Add(citizen); 
        OnCitizenAdded?.Invoke(this, new OnCitizenEventArgs { citizen = citizen });
    }

    private void RemoveCitizenFromList(Transform citizenTransform)
    {
        Citizen citizen = citizenTransform.GetComponent<Citizen>();

        if (citizen == null) return;

        citizenList.Remove(citizen);
        OnCitizenRemoved?.Invoke(this, new OnCitizenEventArgs { citizen = citizen });
    }

    #region CitizenSpawnerManager Subscriptions
    private void CitizenSpawnerManager_OnCitizenSpawned(object sender, CitizenSpawnerManager.OnCitizenSpawnEventArgs e)
    {
        AddCitizenToList(e.citizenTransform);
    }
    private void CitizenSpawnerManager_OnCitizenDespawned(object sender, CitizenSpawnerManager.OnCitizenSpawnEventArgs e)
    {
        RemoveCitizenFromList(e.citizenTransform);
    }
    #endregion
}
