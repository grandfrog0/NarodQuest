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
    public static bool IsTouching { get; private set; }

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
        _inputSystem.Enable();
        _inputSystem.Player.Look.performed += SetDelta;
        _inputSystem.UI.Touch.started += OnTouchStarted;
        _inputSystem.UI.Touch.canceled += OnTouchCanceled;
        _inputSystem.Player.Attack.canceled += OnAttackCanceled;
    }

    void OnDisable()
    {
        _inputSystem.Disable();
        _inputSystem.Player.Look.performed -= SetDelta;
        _inputSystem.Player.Attack.started -= OnTouchStarted;
        _inputSystem.UI.Touch.canceled -= OnTouchCanceled;
        _inputSystem.Player.Attack.canceled -= OnAttackCanceled;
    }

    void SetDelta(InputAction.CallbackContext context)
    {
        PositionDelta = context.ReadValue<Vector2>();
        CurrentTouchPosition = GetCurrentTouchPosition();
    }

    void OnTouchStarted(InputAction.CallbackContext context)
    {
        IsTouching = true;
    }

    void OnTouchCanceled(InputAction.CallbackContext context)
    {
        IsTouching = false;
    }

    void OnAttackCanceled(InputAction.CallbackContext context)
    {
        OnTouch?.Invoke();
        OnTouchAtPosition?.Invoke(GetCurrentTouchPosition());

        IsTouching = false;
    }

    public static Vector2 GetCurrentTouchPosition()
    {
        if (Touchscreen.current == null) 
            return Vector2.zero;

        return Touchscreen.current.primaryTouch.position.ReadValue();
    }
}
