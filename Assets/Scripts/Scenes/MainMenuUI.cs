using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button playButton;

    [Header("Settings")]
    [SerializeField] private string gameScene;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        playButton.onClick.AddListener(PlayGame);
    }

    private void PlayGame() => SceneManager.LoadScene(gameScene);
}
