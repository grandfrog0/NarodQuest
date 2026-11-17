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
    }

    public void Interact()
    {
        SetDynamic(true);

        _parent = transform.parent;
        transform.SetParent(null);

        _isAnimationPlaying = true;
        _animator.Play("trigger");
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
        _arrowRenderer.gameObject.SetActive(value);

        _outlineRenderer.color = value ? _data.OutlineSelectedColor : _data.OutlineHighlightedColor;
        _arrowRenderer.color = value ? _data.ArrowSelectedColor : _data.ArrowHighlightedColor;
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
