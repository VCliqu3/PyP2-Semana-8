using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    public static event EventHandler<OnAnyCitizenConsumptionEventArgs> OnAnyHarvestConsumption;
    public static event EventHandler<OnAnyCitizenConsumptionEventArgs> OnAnyMeatConsumption;
    public static event EventHandler<OnAnyCitizenConsumptionEventArgs> OnAnyFishConsumption;

    public event EventHandler<OnCitizenConsumptionEventArgs> OnMeatConsumption;
    public event EventHandler<OnCitizenConsumptionEventArgs> OnFishConsumption;
    public event EventHandler<OnCitizenConsumptionEventArgs> OnHarvestConsumption;

    public event EventHandler OnCitizenBought;

    private float consumptionTimer;
    private float timer;

    public class OnAnyCitizenConsumptionEventArgs : EventArgs
    {
        public Citizen citizen;
        public int quantity;
    }

    public class OnCitizenConsumptionEventArgs : EventArgs
    {
        public Good good;
        public int quantity;
    }

    private void Start()
    {
        SetRandomConsumptionTimer();
    }

    private void Update()
    {
        HandleGoodsConsumption();
    }

    public void SetWasBought() => OnCitizenBought?.Invoke(this, EventArgs.Empty);

    private void HandleGoodsConsumption()
    {
        if (TimerOnCooldown())
        {
            timer += Time.deltaTime;
            return;
        }

        ConsumeGood();
        ResetTimer();
    }

    private void ConsumeGood()
    {
        int quantity = GetConsumptionQuantity();
        Good good = GetRandomGood();

        switch (good)
        {
            case Good.Meat:
                ConsumeMeat(quantity);
                break;
            case Good.Fish:
                ConsumeFish(quantity);
                break;
            case Good.Harvest:
                ConsumeHarvest(quantity);
                break;
        }
    }

    private int GetConsumptionQuantity()
    {
        int minQuantity = GameManager.Instance.GameSettings.minCitizenConsumptionQuantity;
        int maxQuantity = GameManager.Instance.GameSettings.maxCitizenConsumptionQuantity;

        int randomQuantity = UnityEngine.Random.Range(minQuantity, maxQuantity+1);
        return randomQuantity;
    }

    private Good GetRandomGood()
    {
        float meatConsumptionOdds = GameManager.Instance.GameSettings.meatConsumptionOdds;
        float fishConsumptionOdds = GameManager.Instance.GameSettings.fishConsumptionOdds;
        float harvestConsumptionOdds = GameManager.Instance.GameSettings.harvestConsumptionOdds;

        float oddsAccumulator = meatConsumptionOdds + fishConsumptionOdds + harvestConsumptionOdds;

        float randomOdd = UnityEngine.Random.Range(0f, oddsAccumulator);

        if(randomOdd< meatConsumptionOdds)
        {
            return Good.Meat;
        }

        if (randomOdd < meatConsumptionOdds + fishConsumptionOdds)
        {
            return Good.Fish;
        }

        return Good.Harvest;
    }

    private void ConsumeMeat(int quantity)
    {
        OnAnyMeatConsumption?.Invoke(this, new OnAnyCitizenConsumptionEventArgs { citizen = this, quantity = quantity });
        OnMeatConsumption?.Invoke(this, new OnCitizenConsumptionEventArgs { good = Good.Meat, quantity = quantity });
    }

    private void ConsumeFish(int quantity)
    {
        OnAnyFishConsumption?.Invoke(this, new OnAnyCitizenConsumptionEventArgs { citizen = this, quantity = quantity });
        OnFishConsumption?.Invoke(this, new OnCitizenConsumptionEventArgs { good = Good.Fish, quantity = quantity });
    }

    private void ConsumeHarvest(int quantity)
    {
        OnAnyHarvestConsumption?.Invoke(this, new OnAnyCitizenConsumptionEventArgs { citizen = this, quantity = quantity });
        OnHarvestConsumption?.Invoke(this, new OnCitizenConsumptionEventArgs { good = Good.Harvest, quantity = quantity });
    }

    private void SetRandomConsumptionTimer()
    {
        float minTime = GameManager.Instance.GameSettings.citizenMinConsumptionTime;
        float maxTime = GameManager.Instance.GameSettings.citizenMaxConsumptionTime;    

        consumptionTimer = UnityEngine.Random.Range(minTime, maxTime);
    }

    private bool TimerOnCooldown() => timer < consumptionTimer;
    private void ResetTimer() => timer = 0f;

}
