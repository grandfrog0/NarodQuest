using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSystemManager : MonoBehaviour
{
    public static event UnityAction<Vector2> OnTouch;

    InputSystem_Actions _inputSystem;

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

        OnTouch?.Invoke(screenPosition);
    }
}
