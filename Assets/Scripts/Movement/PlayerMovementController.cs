using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] PlayerConfig config;
    [SerializeField] JoystickController joystick;

    Rigidbody2D _rg;

    void Start()
    {
        Initialize();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Initialize()
    {
        _rg = GetComponent<Rigidbody2D>();
    }

    void Move()
    {
        // итоговое движение
        Vector2 totalMovement = joystick.Movement * config.speed * Time.fixedDeltaTime;
        Vector2 totalPosition = totalMovement + _rg.position;
        _rg.MovePosition(totalPosition);
    }

}
