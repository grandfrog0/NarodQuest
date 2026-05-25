using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementController : MonoBehaviour
{
    public UnityEvent<Vector2> OnMoveAxisChanged { get; } = new();

    [SerializeField] private PlayerConfig config;
    [SerializeField] private JoystickController joystick;
    private Rigidbody2D _rigidbody;

    public Vector2 Axis
    {
        get => _axis;
        set
        {
            _axis = value;
            OnMoveAxisChanged.Invoke(_axis);
        }
    }
    private Vector2 _axis;

    private void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Axis = Vector2.down;
    }

    private void Move()
    {
        Vector2 totalMovement = joystick.Movement * config.speed * Time.fixedDeltaTime;
        Vector2 totalPosition = totalMovement + _rigidbody.position;
        _rigidbody.MovePosition(totalPosition);

        Vector2 axis = totalMovement != Vector2.zero ? Mathf.Abs(totalMovement.x) > Mathf.Abs(totalMovement.y) ? Vector2.right * Mathf.Sign(totalMovement.x) : Vector2.up * Mathf.Sign(totalMovement.y) : Vector2.zero;
        if (Axis != axis && axis != Vector2.zero)
        {
            Axis = axis;
        }
    }
}
