using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SceneLoadChecker : MonoBehaviour
{
    public static event EventHandler<OnSceneLoadEventArgs> OnSceneLoad;

    public class OnSceneLoadEventArgs : EventArgs
    {
        public string sceneName;
    }

    private void Awake()
    {
        CheckLoadScene();
    }

    private void CheckLoadScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        OnSceneLoad?.Invoke(this, new OnSceneLoadEventArgs { sceneName = sceneName });
    }
}
