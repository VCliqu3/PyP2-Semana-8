using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "ScriptableObjects/Game/Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Range(0, 10)] public int startingCitizens;
    [Range(15, 30)] public int citizenMineralPrice;
    [Space]
    [Range(0, 100)] public int startingMeat;
    [Range(0, 100)] public int startingFish;
    [Range(0, 100)] public int startingHarvests;
    [Range(0, 100)] public int startingMinerals;
    [Space]
    [Range(0, 10)] public int meatQuantityPerMeat;
    [Range(0, 10)] public int fishQuantityPerFish;
    [Range(0, 10)] public int harvestQuantityPerHarvest;
    [Range(0, 10)] public int mineralQuantityPerMineral;
    [Space]
    [Range(0f,0.15f)] public float meatProductionPerSecondPerCitizen;
    [Range(0f,0.15f)] public float fishProductionPerSecondPerCitizen;
    [Range(0f,0.15f)] public float harvestsProductionPerSecondPerCitizen;
    [Range(0f,0.15f)] public float mineralsProductionPerSecondPerCitizen;
    [Space]
    [Range(10f, 15f)] public float citizenMinConsumptionTime;
    [Range(25f, 30f)] public float citizenMaxConsumptionTime;
    [Range(0f, 1f)] public float meatConsumptionOdds;
    [Range(0f, 1f)] public float fishConsumptionOdds;
    [Range(0f, 1f)] public float harvestConsumptionOdds;
    [Space]
    [Range(1f, 2f)] public int minCitizenConsumptionQuantity;
    [Range(2f, 3f)] public int maxCitizenConsumptionQuantity;
    [Space]
    [Range(105, 200)] public int meatToWin;
    [Range(105, 200)] public int fishToWin;
    [Range(105, 200)] public int harvestToWin;
}
