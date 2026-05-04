using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] float maxDistance;

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

        Vector2 direction = _startPosition - targetPosition;
        direction.Normalize();

        float distance = Vector3.Distance(_startPosition, targetPosition);
        float clampedDistance = Mathf.Clamp(distance, 0f, maxDistance);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
