using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizensManager : MonoBehaviour
{
    public static CitizensManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private List<Citizen> citizenList;

    public List<Citizen> CitizenList => citizenList;

    private void OnEnable()
    {
        Citizen.OnCitizenSpawned += Citizen_OnCitizenSpawned;
    }

    private void OnDisable()
    {
        Citizen.OnCitizenSpawned -= Citizen_OnCitizenSpawned;
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

    private void AddCitizen(Citizen citizen)
    {
        if (citizenList.Contains(citizen)) return;
        citizenList.Add(citizen);
    }

    private void RemoveCitizen(Citizen citizen)
    {
        if (!citizenList.Contains(citizen)) return;
        citizenList.Remove(citizen);
    }

    private void Citizen_OnCitizenSpawned(object sender, Citizen.OnCitizenEventArgs e)
    {
        AddCitizen(e.citizen);
    }
}
