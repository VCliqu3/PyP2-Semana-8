using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    public static event EventHandler<OnCitizenEventArgs> OnHarvestConsumption;
    public static event EventHandler<OnCitizenEventArgs> OnMeatConsumption;
    public static event EventHandler<OnCitizenEventArgs> OnMineralConsumption;
    public static event EventHandler<OnCitizenEventArgs> OnFishConsumption;

    public class OnCitizenEventArgs : EventArgs
    {
        public Citizen citizen;
    }

    private void Update()
    {
        HandleGoodsConsumption();
    }

    private void HandleGoodsConsumption()
    {
        
    }
}
