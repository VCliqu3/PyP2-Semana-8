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

    public Vector2 GetMovementVectorNormalized()
    {
        if (!CanProcessInput()) return Vector2.zero;

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector.Normalize();
        return inputVector;
    }

    public bool GetFireInput()
    {
        if (!CanProcessInput()) return false;

        bool fire = playerInputActions.Player.Fire.IsPressed();

        return fire;
    }

    public Vector3 GetWorldMousePosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);
}
