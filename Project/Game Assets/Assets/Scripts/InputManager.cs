using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputAction _mousePositionAction;
    private InputAction _mouseaction;
    public static PlayerInput PlayerInput;
    public static Vector2 MousePosition;
    public static bool WasLeftMousebuttonpressed;
    public static bool WasLeftMousebuttonreleased;
    public static bool IsLeftmouspressed;
    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        _mousePositionAction = PlayerInput.actions["MousePosition"];
        _mouseaction = PlayerInput.actions["Mouse"];
    }
    private void Update()
    {
        MousePosition = _mousePositionAction.ReadValue<Vector2>();

        WasLeftMousebuttonpressed = _mouseaction.WasPressedThisFrame();
        WasLeftMousebuttonreleased = _mouseaction.WasReleasedThisFrame();
        IsLeftmouspressed = _mouseaction.IsPressed();
    }
}

