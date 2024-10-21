using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button menuButton;

    [Header("Settings")]
    [SerializeField] private string menuScene;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        menuButton.onClick.AddListener(BackToMenu);
    }

    private void BackToMenu() => SceneManager.LoadScene(menuScene);
}
