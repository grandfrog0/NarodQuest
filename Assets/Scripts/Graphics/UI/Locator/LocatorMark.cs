using UnityEngine;

public class LocatorMark : MonoBehaviour
{
    [SerializeField] private GameObject _graphics;
    [SerializeField] private float _offset;
    private RectTransform _rect;
    private Transform _target;
    private bool _isTargetOnScreen;

    float _halfWidth;
    float _halfHeight;

    public Transform Target
    {
        get => _target;
        set
        {
            gameObject.SetActive(true);
            _target = value;
        }
    }

    public void Initialize()
    {
        _rect = GetComponent<RectTransform>();
        _halfWidth = Screen.width / 2 - _offset;
        _halfHeight = Screen.height / 2 - _offset;
    }

    public void Handle(float deltaTime)
    {
        if (_target == null)
        {
            return;
        }

        Vector3 targetPosition = Camera.main.WorldToScreenPoint(_target.position);

        bool isOnScreen = IsOnScreen(targetPosition);

        if (isOnScreen != _isTargetOnScreen)
        {
            _isTargetOnScreen = isOnScreen;
            _graphics.gameObject.SetActive(!isOnScreen);
        }

        if (!isOnScreen)
        {
            targetPosition = Clamp(targetPosition);
            _rect.anchoredPosition = targetPosition;
            _rect.up = Vector2.Lerp(_rect.up, -targetPosition, deltaTime * 3);
        }
    }

    private bool IsOnScreen(Vector3 position)
    {
        return position.x >= 0 && position.x < Screen.width && position.y >= 0 && position.y < Screen.height;
    }

    private Vector2 Clamp(Vector3 position)
    {
        position.x -= _halfWidth;
        position.y -= _halfHeight;

        float multiplier = Mathf.Min(Mathf.Abs(_halfHeight / position.y), Mathf.Abs(_halfWidth / position.x));
        float magnitude = position.magnitude * multiplier;

        return Vector2.ClampMagnitude(position, magnitude);
    }

    public void Clear()
    {
        _target = null;
        gameObject.SetActive(false);
    }
}
