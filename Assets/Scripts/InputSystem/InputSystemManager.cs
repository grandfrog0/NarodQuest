using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSystemManager : MonoBehaviour
{
    public static event UnityAction<Vector2> OnTouchAtPosition;
    public static event UnityAction OnTouch;

    InputSystem_Actions _inputSystem;
    Vector2 _prevPosition;

    public static Vector2 PositionDelta { get; private set; }

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
        _inputSystem.Player.Attack.performed += OnAttack;
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
        Vector2 currentPosition = GetCurrentTouchPosition();
        PositionDelta = currentPosition - _prevPosition;
        _prevPosition = currentPosition;
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
