using UnityEngine;
using Zenject;

public class PlayerInputRouter : MonoBehaviour
{
    private InputSystemManager _inputSystem;
    private JoystickController _moveJoystick;
    private PlayerMovementController _playerMove;

    [Inject]
    private void Construct(InputSystemManager inputSystem, JoystickController moveJoystick, PlayerMovementController playerMove)
    {
        _inputSystem = inputSystem;
        _moveJoystick = moveJoystick;
        _playerMove = playerMove;
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;

        if (_inputSystem.MoveDirection != Vector2.zero)
        {
            direction = _inputSystem.MoveDirection;
        }
        else if (_moveJoystick.Movement != Vector2.zero)
        {
            direction = _moveJoystick.Movement;
        }

        _playerMove.Move(direction);
    }
}
