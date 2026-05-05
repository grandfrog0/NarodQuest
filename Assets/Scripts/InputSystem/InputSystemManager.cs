using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSystemManager : MonoBehaviour
{
    public static event UnityAction<Vector2> OnTouchAtPosition;
    public static event UnityAction OnTouch;

    InputSystem_Actions _inputSystem;

    void Awake()
    {
        _inputSystem = new();
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

    void OnAttack(InputAction.CallbackContext context)
    {
        if (Touchscreen.current == null) return;
        
        Vector2 screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        OnTouch?.Invoke();
        OnTouchAtPosition?.Invoke(screenPosition);
    }
}
