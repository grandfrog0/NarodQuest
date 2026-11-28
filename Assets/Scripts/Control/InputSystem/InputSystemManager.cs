using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

public class InputSystemManager : IInitializable, IDisposable
{
    public UnityEvent<Vector2> OnTouchAtPosition { get; } = new();
    public UnityEvent OnTouch { get; } = new();

    public Vector2 PositionDelta { get; private set; }
    public Vector2 CurrentTouchPosition { get; private set; }
    public bool IsTouching { get; private set; }
    public Vector2 MoveDirection { get; private set; }

    public Vector2 GetCurrentTouchPosition()
    {
        if (Touchscreen.current == null)
        {
            return Vector2.zero;
        }

        return Touchscreen.current.primaryTouch.position.ReadValue();
    }

    private InputSystem_Actions _inputSystem;

    public InputSystemManager()
    {
        _inputSystem = new();
    }

    public void Initialize()
    {
        _inputSystem.Enable();

        _inputSystem.UI.Touch.started += OnTouchStarted;
        _inputSystem.UI.Touch.canceled += OnTouchCanceled;

        _inputSystem.Player.Look.performed += SetDelta;

        _inputSystem.Player.Attack.started += OnTouchStarted;
        _inputSystem.Player.Attack.canceled += OnAttackCanceled;

        _inputSystem.Player.Move.performed += OnMovePerformed;
        _inputSystem.Player.Move.canceled += OnMoveCancelled;
    }

    public void Dispose()
    {
        _inputSystem.Disable();

        _inputSystem.UI.Touch.started -= OnTouchStarted;
        _inputSystem.UI.Touch.canceled -= OnTouchCanceled;

        _inputSystem.Player.Look.performed -= SetDelta;

        _inputSystem.Player.Attack.started -= OnTouchStarted;
        _inputSystem.Player.Attack.canceled -= OnAttackCanceled;

        _inputSystem.Player.Move.performed -= OnMovePerformed;
        _inputSystem.Player.Move.canceled -= OnMoveCancelled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
    }

    private void OnMoveCancelled(InputAction.CallbackContext context)
    {
        MoveDirection = Vector2.zero;
    }

    private void SetDelta(InputAction.CallbackContext context)
    {
        PositionDelta = context.ReadValue<Vector2>();
        CurrentTouchPosition = GetCurrentTouchPosition();
    }

    private void OnTouchStarted(InputAction.CallbackContext context)
    {
        IsTouching = true;
    }

    private void OnTouchCanceled(InputAction.CallbackContext context)
    {
        IsTouching = false;
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        OnTouch?.Invoke();
        OnTouchAtPosition?.Invoke(GetCurrentTouchPosition());

        IsTouching = false;
    }
}
