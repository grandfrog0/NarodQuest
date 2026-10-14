using System;
using UnityEngine;

/// <summary>
/// Объект, который можно тащить и ставить
/// </summary>
[RequireComponent(typeof(ItemRenderer))]
public class PlayerBringable : MonoBehaviour
{
    public event Action<PlayerBringable> OnDrag;
    public event Action<PlayerBringable> OnDrop;

    private Transform _defaultParent;

    public bool IsActive { get; set; } = true;
    public ItemRenderer Renderer
    {
        get
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<ItemRenderer>();
            }
            return _renderer;
        }
    }
    private ItemRenderer _renderer;

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
        OnDrag?.Invoke(this);
    }

    public void Drop()
    {
        transform.SetParent(_defaultParent);

        if (Renderer != null)
        {
            Renderer.SortingOrder = 0;
        }

        OnDrop?.Invoke(this);
    }
}
