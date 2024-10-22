using GameJolt.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyAchieverHandler : MonoBehaviour
{
    private const int MEAT_TROPHY_ID = 248167;
    private const int FISH_TROPHY_ID = 248168;
    private const int HARVEST_TROPHY_ID = 248169;
    private const int MINERAL_TROPHY_ID = 248170;
    private const int CITIZEN_TROPHY_ID = 248171;

    private const int MEAT_TROPHY_QUANTITY = 100;
    private const int FISH_TROPHY_QUANTITY = 100;
    private const int HARVEST_TROPHY_QUANTITY = 100;
    private const int MINERAL_TROPHY_QUANTITY = 100;
    private const int CITIZEN_TROPHY_QUANTITY = 10;

    private void OnEnable()
    {
        MeatManager.OnMeatIncreased += MeatManager_OnMeatIncreased;
        FishManager.OnFishIncreased += FishManager_OnFishIncreased;
        HarvestsManager.OnHarvestsIncreased += HarvestsManager_OnHarvestsIncreased;
        MineralsManager.OnMineralsIncreased += MineralsManager_OnMineralsIncreased;
        CitizensManager.OnCitizenBought += CitizensManager_OnCitizenBought;
    }

    private void OnDisable()
    {
        MeatManager.OnMeatIncreased -= MeatManager_OnMeatIncreased;
        FishManager.OnFishIncreased -= FishManager_OnFishIncreased;
        HarvestsManager.OnHarvestsIncreased -= HarvestsManager_OnHarvestsIncreased;
        MineralsManager.OnMineralsIncreased -= MineralsManager_OnMineralsIncreased;
        CitizensManager.OnCitizenBought -= CitizensManager_OnCitizenBought;
    }

    private void Start()
    {
        Trophies.Remove(MEAT_TROPHY_ID);
        Trophies.Remove(FISH_TROPHY_ID);
        Trophies.Remove(HARVEST_TROPHY_ID);
        Trophies.Remove(MINERAL_TROPHY_ID);
        Trophies.Remove(CITIZEN_TROPHY_ID);
    }

    private void CheckUnlockMeatTrophy(int meat)
    {
        if (meat < MEAT_TROPHY_QUANTITY) return;

        Trophies.TryUnlock(MEAT_TROPHY_ID);
    }

    private void CheckUnlockFishTrophy(int fish)
    {
        if (fish < FISH_TROPHY_QUANTITY) return;

        Trophies.TryUnlock(FISH_TROPHY_ID);
    }

    private void CheckUnlockHarvestTrophy(int harvest)
    {
        if (harvest < HARVEST_TROPHY_QUANTITY) return;

        Trophies.TryUnlock(HARVEST_TROPHY_ID);
    }

    private void CheckUnlockMineralsTrophy(int minerals)
    {
        if (minerals < MINERAL_TROPHY_QUANTITY) return;

        Trophies.TryUnlock(MINERAL_TROPHY_ID);
    }

    private void CheckUnlockCitizenTrophy(int citizen)
    {
        if (citizen < CITIZEN_TROPHY_QUANTITY) return;

        Trophies.TryUnlock(CITIZEN_TROPHY_ID);
    }

    private void MeatManager_OnMeatIncreased(object sender, MeatManager.OnMeatEventArgs e)
    {
        CheckUnlockMeatTrophy(e.meat);
    }
    private void FishManager_OnFishIncreased(object sender, FishManager.OnFishEventArgs e)
    {
        CheckUnlockFishTrophy(e.fish);
    }
    private void HarvestsManager_OnHarvestsIncreased(object sender, HarvestsManager.OnHarvestsEventArgs e)
    {
        CheckUnlockHarvestTrophy(e.harvests);
    }
    private void MineralsManager_OnMineralsIncreased(object sender, MineralsManager.OnMineralsEventArgs e)
    {
        CheckUnlockMineralsTrophy(e.minerals);
    }
    private void CitizensManager_OnCitizenBought(object sender, CitizensManager.OnCitizenBoughtEventArgs e)
    {
        CheckUnlockCitizenTrophy(e.citizenCount);
    }
}
