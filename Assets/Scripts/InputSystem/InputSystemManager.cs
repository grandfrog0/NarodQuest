using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSystemManager : MonoBehaviour
{
    public static UnityEvent<Vector2> OnTouchAtPosition = new();
    public static UnityEvent OnTouch = new();

    InputSystem_Actions _inputSystem;
    Vector2 _prevPosition;

    public static Vector2 PositionDelta { get; private set; }
    public static Vector2 CurrentTouchPosition { get; private set; }

    void Awake()
    {
        _inputSystem = new();
    }

    void Start()
    {
        _prevPosition = GetCurrentTouchPosition();
    }

    void OnEnable()
    {
        _inputSystem.Player.Enable();
        _inputSystem.Player.Attack.canceled += OnAttack;
    }

    void OnDisable()
    {
        _inputSystem.Player.Disable();
        _inputSystem.Player.Attack.performed -= OnAttack;
    }

    void Update()
    {
        CalculateDelta();
    }

    void CalculateDelta()
    {
        CurrentTouchPosition = GetCurrentTouchPosition();
        PositionDelta = CurrentTouchPosition - _prevPosition;
        _prevPosition = CurrentTouchPosition;
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        OnTouch?.Invoke();
        OnTouchAtPosition?.Invoke(GetCurrentTouchPosition());
    }

    public static Vector2 GetCurrentTouchPosition()
    {
        if (Touchscreen.current == null) 
            return Vector2.zero;

        return Touchscreen.current.primaryTouch.position.ReadValue();
    }
}
