using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string nextScene;
    [SerializeField] private string defeatScene;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

        
    private void GoToNextScene() => SceneManager.LoadScene(nextScene);
    private void GoToDefeatScene() => SceneManager.LoadScene(defeatScene);

}
