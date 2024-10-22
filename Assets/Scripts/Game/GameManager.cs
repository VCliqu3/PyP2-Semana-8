using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private string nextScene;
    [SerializeField] private string defeatScene;
    [SerializeField] private GameSettingsSO gameSettingsSO;

    public GameSettingsSO GameSettings => gameSettingsSO;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

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

    private void GoToNextScene() => SceneManager.LoadScene(nextScene);
    private void GoToDefeatScene() => SceneManager.LoadScene(defeatScene);

}
