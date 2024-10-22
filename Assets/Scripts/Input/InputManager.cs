using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        SetSingleton();
        InitializePlayerInputActions();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one InputManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializePlayerInputActions()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public bool CanProcessInput()
    {
        return true;
    }


    public bool GetInteractionInputDown()
    {
        if (!CanProcessInput()) return false;

        bool interact = playerInputActions.Player.Interact.WasPerformedThisFrame();

        return interact;
    }

    public Vector3 GetWorldMousePosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);
}
