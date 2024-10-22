using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "ScriptableObjects/Game/Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Range(0, 10)] public int startingCitizens;
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
    [Range(0f,0.05f)] public float meatProductionPerSecondPerCitizen;
    [Range(0f,0.05f)] public float fishProductionPerSecondPerCitizen;
    [Range(0f,0.05f)] public float harvestsProductionPerSecondPerCitizen;
    [Range(0f,0.05f)] public float mineralsProductionPerSecondPerCitizen;
    [Space]
    [Range(0f, 0.05f)] public float meatComsumptionPerSecondPerCitizen;
    [Range(0f, 0.05f)] public float fishComsumptionPerSecondPerCitizen;
    [Range(0f, 0.05f)] public float harvestsComsumptionPerSecondPerCitizen;
    [Range(0f, 0.05f)] public float mineralsComsumptionPerSecondPerCitizen;
}
