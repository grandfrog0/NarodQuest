using UnityEngine;

public class PlayerInputRouter : MonoBehaviour
{
    [SerializeField] private InputSystemManager _inputSystemManager;
    [SerializeField] private JoystickController _moveJoystick;
    [SerializeField] private PlayerMovementController _playerMovementController;

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;

        if (InputSystemManager.MoveDirection != Vector2.zero)
        {
            direction = InputSystemManager.MoveDirection;
        }
        else if (_moveJoystick.Movement != Vector2.zero)
        {
            direction = _moveJoystick.Movement;
        }

        _playerMovementController.Move(direction);
    }
}
