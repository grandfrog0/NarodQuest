using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] bool xInverse;
    [SerializeField] float maxDistance;

    public Vector2 Movement { get; private set; }

    RectTransform _joystick;
    Vector2 _startPosition;

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        _joystick = GetComponent<RectTransform>();
        _startPosition = _joystick.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 targetPosition = eventData.position;

        // инверсия в зависимости от якорей джостика
        targetPosition.x -= xInverse ? Screen.width : 0f;

        Vector2 direction = targetPosition - _startPosition;
        direction.Normalize();
        // уменьшение магнитуды для реализации разгона
        direction = Vector2.ClampMagnitude(direction, 1 / maxDistance);

        Debug.Log(direction);

        float distance = Vector3.Distance(_startPosition, targetPosition);
        float clampedDistance = Mathf.Clamp(distance, 0f, maxDistance);
        
        Movement = direction * clampedDistance;
        _joystick.anchoredPosition = _startPosition + Movement * clampedDistance;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _joystick.anchoredPosition = _startPosition;
        Movement = Vector2.zero;
    }
}
