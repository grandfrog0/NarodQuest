using System;
using UnityEngine;

/// <summary>
/// Объект, который можно тащить и ставить
/// </summary>
public class PlayerBringable : MonoBehaviour
{
    public event Action OnDrag;
    public event Action OnDrop;

    private Transform _defaultParent;

    public bool IsActive { get; set; } = true;

    [SerializeField] private Vector2 _offsetHorizontal;
    [SerializeField] private Vector2 _offsetVertical;
    [SerializeField] private Vector2 _offset;
    public Vector2 GetOffset(Vector2 axis) => _offsetHorizontal * axis.x + _offsetVertical * axis.y + _offset;

    private void Start()
    {
        _defaultParent = transform.parent;
    }

    public void Drag(Transform currentParent)
    {
        transform.SetParent(currentParent);
        OnDrag?.Invoke();
    }

    public void Drop()
    {
        transform.SetParent(_defaultParent);
        OnDrop?.Invoke();
    }
}
