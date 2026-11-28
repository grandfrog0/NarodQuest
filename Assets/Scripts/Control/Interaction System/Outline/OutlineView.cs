using System;
using UnityEngine;
using Zenject;

public class OutlineView : MonoBehaviour, IPoolable<OutlineData, Transform, IMemoryPool>, IDisposable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _outlineRenderer;
    [SerializeField] private Transform _arrowOffset;
    [SerializeField] private SpriteRenderer _arrowRenderer;

    private IMemoryPool _pool;

    private OutlineData _data;

    public OutlineState State
    {
        get => _state;
        set
        {
            _state = value;

            if (_isAnimationPlaying)
            {
                return;
            }

            switch (_state)
            {
                case OutlineState.Disposed:
                    Dispose();
                    break;

                case OutlineState.Highlighted:
                    SetDynamic(false);
                    break;

                case OutlineState.Selected:
                    SetDynamic(true);
                    break;
            }
        }
    }
    private OutlineState _state;

    private bool _isDynamic;

    public bool IsForced => _isForced;
    private bool _isForced;

    private Transform _parent;
    private bool _isAnimationPlaying = false;

    private void Initialize(OutlineData data, Transform parent)
    {
        _data = data;

        _outlineRenderer.sprite = _data.OutlineSprite;
        _outlineRenderer.color = _data.OutlineSelectedColor;
        _outlineRenderer.sortingOrder = _data.SortingOrder;
        _arrowRenderer.sprite = _data.ArrowSprite;
        _arrowRenderer.color = _data.ArrowSelectedColor;

        _arrowOffset.localPosition = _data.ArrowOffset;

        transform.SetParent(parent);
        transform.localPosition = _data.OutlineOffset;
        transform.localScale = _data.OutlineSize;

        _isForced = false;
    }

    public void Interact()
    {
        SetDynamic(true);

        _parent = transform.parent;
        transform.SetParent(null);

        _isAnimationPlaying = true;
        _animator.Play("trigger");
    }

    public void SetForced(bool value)
    {
        _isForced = value;
        Refresh();
    }

    public void OnAnimationFinished()
    {
        _isAnimationPlaying = false;

        if (_parent != null)
        {
            transform.SetParent(_parent);
            transform.localPosition = _data.OutlineOffset;
            _animator.Play("pulse");
        }

        State = State;
    }

    public void Dispose()
    {
        if (_isAnimationPlaying)
        {
            _parent = null;
            State = OutlineState.Disposed;
        }
        else
        {
            //Destroy(gameObject);
            _pool.Despawn(this);
        }
    }

    private void SetDynamic(bool value)
    {
        _isDynamic = value;
        Refresh();
    }

    private void Refresh()
    {
        _arrowRenderer.gameObject.SetActive(_isForced || _isDynamic);

        if (_isForced)
        {
            _outlineRenderer.color = _isDynamic ? _data.OutlineForcedColor : _data.OutlineHighlightedForcedColor;
            _arrowRenderer.color = _isDynamic ? _data.ArrowForcedColor : _data.ArrowHighlightedForcedColor;
        }
        else
        {
            _outlineRenderer.color = _isDynamic ? _data.OutlineSelectedColor : _data.OutlineHighlightedColor;
            _arrowRenderer.color = _isDynamic ? _data.ArrowSelectedColor : _data.ArrowHighlightedColor;
        }
    }

    public void OnSpawned(OutlineData data, Transform parent, IMemoryPool pool)
    {
        _pool = pool;

        gameObject.SetActive(true);
        Initialize(data, parent);
    }

    public void OnDespawned()
    {
        _data = null;
        transform.parent = null;

        gameObject.SetActive(false);
    }

    public class Factory : PlaceholderFactory<OutlineData, Transform, OutlineView> { }
}
