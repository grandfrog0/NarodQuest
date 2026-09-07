using UnityEngine;
using UnityEngine.Rendering.UI;

public class LocatorMark : MonoBehaviour
{
    [SerializeField] RectTransform _rect;
    [SerializeField] GameObject _graphics;
    [SerializeField] Vector2 _offset;
    private Transform _target;
    private bool _isTargetOnScreen;

    private void FixedUpdate()
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
            _rect.anchoredPosition = Clamp(targetPosition);
        }
    }

    private bool IsOnScreen(Vector3 position)
    {
        return position.x >= 0 && position.x < Screen.width && position.y >= 0 && position.y < Screen.height;
    }

    private Vector2 Clamp(Vector3 position)
    {
        return new Vector2(Mathf.Clamp(position.x - Screen.width / 2, Screen.width / -2 + _offset.x, Screen.width / 2 - _offset.x), Mathf.Clamp(position.y - Screen.height / 2, Screen.height / -2 + _offset.y, Screen.height / 2 - _offset.y));
    }

    public void SetTarget(Transform target)
    {
        gameObject.SetActive(true);
        _target = target;
    }

    public void Clear()
    {
        _target = null;
        gameObject.SetActive(false);
    }
}
