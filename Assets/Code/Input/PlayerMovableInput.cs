using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovableInput : IInput, IUIInput
{
    // Transport Controls
    public float Movement => _moveAction.ReadValue<float>();

    public float Rotation => _rotationAction.ReadValue<float>();

    public bool Handbrake => _handbreakAction.IsPressed();

    public bool Brake => _breakAction.IsPressed();

    // Weapon Controls
    public Vector2 AimDirection => _aimAction.ReadValue<Vector2>();

    public bool Shoot => _shootAction.IsPressed();

    // UI Controls
    public bool PauseButtonPressed => _pauseAction.WasPressedThisFrame();


    private readonly PlayerInput _playerInput;

    private readonly InputAction _moveAction;

    private readonly InputAction _rotationAction;

    private readonly InputAction _handbreakAction;
    private readonly InputAction _breakAction;

    private readonly InputAction _aimAction;
    private readonly InputAction _shootAction;

    private readonly InputAction _pauseAction;

    public PlayerMovableInput(PlayerInput playerInput)
    {
        _playerInput = playerInput;

        _moveAction = _playerInput.actions["Move"];

        _rotationAction = _playerInput.actions["Rotation"];

        _handbreakAction = _playerInput.actions["Break"];
        _breakAction = _playerInput.actions["Break2"];

        _aimAction = _playerInput.actions["Aim"];
        _shootAction = _playerInput.actions["Shoot"];

        _pauseAction = _playerInput.actions["Pause"];
    }
}
