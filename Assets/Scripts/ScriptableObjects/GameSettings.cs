using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "ScriptableObjects/Game/Settings")]
public class GameSettings : ScriptableObject
{
    [Range(0, 10)] public int startingCitizens;
    [Range(0, 100)] public int startingMeat;
    [Range(0, 100)] public int startingFish;
    [Range(0, 100)] public int startingHarvests;
    [Range(0, 100)] public int startingMinerals;
}
