using Code.Infrastructure.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerInput : IInput, IUIInput
{
    // Transport Controls
    public float Movement => _movementAction.ReadValue<float>();

    public float Rotation => _rotationAction.ReadValue<float>();

    public bool Handbrake => _handbreakAction.IsPressed();

    public bool Brake => _breakAction.IsPressed();

    // Weapon Controls
    public Vector2 AimTargetPoint
    {
        get
        {
            Vector2 targetPoint = Camera.main.ScreenToWorldPoint(_aimTargetPointAction.ReadValue<Vector2>());

            return targetPoint;
        }
    }
    public bool Shoot => _shootAction.IsPressed();

    // UI Controls
    public bool PauseButtonPressed => _pauseAction.WasPressedThisFrame();


    private readonly PlayerInput _playerInput;

    private readonly InputAction _movementAction;

    private readonly InputAction _rotationAction;

    private readonly InputAction _handbreakAction;
    private readonly InputAction _breakAction;

    private readonly InputAction _aimTargetPointAction;
    private readonly InputAction _shootAction;

    private readonly InputAction _pauseAction;

    public MainPlayerInput(PlayerInput playerInput, PlayerInputConfig config)
    {
        _playerInput = playerInput;

        _movementAction = _playerInput.actions[config.MovementActionName];

        _rotationAction = _playerInput.actions[config.RotationActionName];

        _handbreakAction = _playerInput.actions[config.HandbrakeActionName];
        _breakAction = _playerInput.actions[config.BrakeActionName];

        _aimTargetPointAction = _playerInput.actions[config.AimTargetPointActionName];
        _shootAction = _playerInput.actions[config.ShootActionName];

        _pauseAction = _playerInput.actions[config.PauseButtonActionName];
    }
}
