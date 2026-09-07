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
