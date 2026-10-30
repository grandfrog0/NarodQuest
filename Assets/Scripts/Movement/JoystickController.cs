using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public event Action OnDragStart;
    public event Action OnPerformed;
    public event Action OnClick;
    public event Action OnCancelled;
    public event Action<Vector2> OnDragging;

    [SerializeField] bool xInverse;
    [SerializeField] float maxDistance;
    [SerializeField] float _cancelDistance = 50f;

    public Vector2 Movement { get; private set; }

    private RectTransform _joystick;
    private Vector2 _startPosition;
    private float _widthAnchor;

    private bool _isCancelled;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        _joystick = GetComponent<RectTransform>();
        _startPosition = _joystick.anchoredPosition;
        _widthAnchor = xInverse ? Screen.width : 0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 targetPosition = eventData.position;

        // инверсия в зависимости от якорей джойстика
        targetPosition.x -= _widthAnchor;

        Vector2 direction = targetPosition - _startPosition;
        direction.Normalize();
        // уменьшение магнитуды для реализации разгона
        direction = Vector2.ClampMagnitude(direction, 1 / maxDistance);

        float distance = Vector3.Distance(_startPosition, targetPosition);
        float clampedDistance = Mathf.Clamp(distance, 0f, maxDistance);

        if (clampedDistance < _cancelDistance)
        {
            clampedDistance = 0f;

            if (!_isCancelled)
            {
                OnCancelled?.Invoke();
                _isCancelled = true;
            }
        }
        else
        {
            if (_isCancelled)
            {
                OnDragStart?.Invoke();
                _isCancelled = false;
            }
        }

        Movement = direction * clampedDistance;
        _joystick.anchoredPosition = _startPosition + Movement * clampedDistance;

        OnDragging?.Invoke(Movement);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isCancelled)
        {
            OnPerformed?.Invoke();
        }
        Clear();
    }

    private void Clear()
    {
        _joystick.anchoredPosition = _startPosition;
        Movement = Vector2.zero;
    }

    public void SetActive(bool value)
    {
        if (value)
        {
            gameObject.SetActive(true);
        }
        else
        {
            Clear();
            gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isCancelled)
        {
            OnClick?.Invoke();
        }
    }
}
