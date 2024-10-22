using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatSpawner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private List<MeatSpawnPosition> meatSpawnPositions;

    private float timer = 0f;

    public class MeatSpawnPosition
    {
        public Transform meatPosition;
        public Meat meat;
    }

    private void Update()
    {
        HandleMeatSpawn();
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

    private void SpawnMeat()
    {
        Debug.Log("Spawn");
    }

    private float CalculateTimeToSpawn()
    {
        float spawnRate = CitizensManager.Instance.CitizenCount * GameManager.Instance.GameSettings.meatProductionPerSecondPerCitizen;
        return 1 / spawnRate;
    }

    private void ResetTimer() => timer = 0f;
    private bool TimerOnCooldown() => timer < CalculateTimeToSpawn();
}
