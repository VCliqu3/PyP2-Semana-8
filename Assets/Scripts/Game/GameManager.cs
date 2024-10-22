using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private string victoryScene;
    [SerializeField] private string defeatScene;
    [SerializeField] private GameSettingsSO gameSettingsSO;

    public GameSettingsSO GameSettings => gameSettingsSO;

    private void OnEnable()
    {
        MeatManager.OnMeatIncreased += MeatManager_OnMeatIncreased;
        MeatManager.OnMeatDecreased += MeatManager_OnMeatDecreased;

        FishManager.OnFishIncreased += FishManager_OnFishIncreased;
        FishManager.OnFishDecreased += FishManager_OnFishDecreased;

        HarvestsManager.OnHarvestsIncreased += HarvestsManager_OnHarvestsIncreased;
        HarvestsManager.OnHarvestsDecreased += HarvestsManager_OnHarvestsDecreased;
    }

    private void OnDisable()
    {
        MeatManager.OnMeatIncreased -= MeatManager_OnMeatIncreased;
        MeatManager.OnMeatDecreased -= MeatManager_OnMeatDecreased;

        FishManager.OnFishIncreased -= FishManager_OnFishIncreased;
        FishManager.OnFishDecreased -= FishManager_OnFishDecreased;

        HarvestsManager.OnHarvestsIncreased -= HarvestsManager_OnHarvestsIncreased;
        HarvestsManager.OnHarvestsDecreased -= HarvestsManager_OnHarvestsDecreased;
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
            Debug.LogWarning("There is more than one GameManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void CheckNoGoods()
    {
        int meatQuantity = MeatManager.Instance.Meat;
        int fishQuantity = FishManager.Instance.Fish;
        int harvestQuantity = HarvestsManager.Instance.Harvests;

        if (meatQuantity > 0 && fishQuantity > 0 && harvestQuantity > 0) return;

        GoToDefeatScene();
    }

    private void CheckReachedGoods()
    {
        int meatQuantity = MeatManager.Instance.Meat;
        int fishQuantity = FishManager.Instance.Fish;
        int harvestQuantity = HarvestsManager.Instance.Harvests;

        if (meatQuantity < gameSettingsSO.meatToWin || fishQuantity < gameSettingsSO.fishToWin || harvestQuantity < gameSettingsSO.harvestToWin) return;

        GoToVictoryScene();
    }

    private void GoToVictoryScene() => SceneManager.LoadScene(victoryScene);
    private void GoToDefeatScene() => SceneManager.LoadScene(defeatScene);

    #region Subscriptions
    private void MeatManager_OnMeatIncreased(object sender, MeatManager.OnMeatEventArgs e)
    {
        CheckReachedGoods();
    }
    private void MeatManager_OnMeatDecreased(object sender, MeatManager.OnMeatEventArgs e)
    {
        CheckNoGoods();
    }
    private void FishManager_OnFishIncreased(object sender, FishManager.OnFishEventArgs e)
    {
        CheckReachedGoods();
    }
    private void FishManager_OnFishDecreased(object sender, FishManager.OnFishEventArgs e)
    {
        CheckNoGoods();
    }
    private void HarvestsManager_OnHarvestsIncreased(object sender, HarvestsManager.OnHarvestsEventArgs e)
    {
        CheckReachedGoods();
    }
    private void HarvestsManager_OnHarvestsDecreased(object sender, HarvestsManager.OnHarvestsEventArgs e)
    {
        CheckNoGoods();
    }
    #endregion
}
