using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementController : MonoBehaviour
{
    public static PlayerMovementController Instance { get; private set; }

    public UnityEvent<Vector2> OnMovementAxisChanged { get; } = new();

    [SerializeField] PlayerConfig config;
    [SerializeField] JoystickController joystick;

    public float SpeedMultipler { get; set; } = 1;

    Rigidbody2D _rg;

    public Vector2 Axis { get; private set; } = Vector2.down;

    void Start()
    {
        Instance = this;
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
        Vector2 totalMovement = joystick.Movement * config.speed * SpeedMultipler * Time.fixedDeltaTime;
        Vector2 totalPosition = totalMovement + _rg.position;
        _rg.MovePosition(totalPosition);

        Vector2 axis = totalMovement == Vector2.zero ? Vector2.zero :
            Mathf.Abs(totalMovement.x) > Mathf.Abs(totalMovement.y) ?
            Vector2.right * Mathf.Sign(totalMovement.x) : Vector2.up * Mathf.Sign(totalMovement.y);

        if (axis != Vector2.zero && axis != Axis)
        {
            Axis = axis;
            OnMovementAxisChanged.Invoke(Axis);
        }
    }

}
