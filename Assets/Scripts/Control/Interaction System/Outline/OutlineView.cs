using System;
using UnityEngine;
using Zenject;

public class OutlineView : MonoBehaviour, IPoolable<OutlineConfig, Transform, IMemoryPool>, IDisposable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _outlineRenderer;
    [SerializeField] private Transform _arrowOffset;
    [SerializeField] private SpriteRenderer _arrowRenderer;

    private IMemoryPool _pool;

    private OutlineConfig _config;
    private Color _highlightedOutlineColor;

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

    private void Initialize(OutlineConfig config, Transform parent)
    {
        _config = config;

        _highlightedOutlineColor = new Color(
            _config.OutlineColor.r,
            _config.OutlineColor.g,
            _config.OutlineColor.b,
            _config.OutlineColor.a * 0.34f
            );

        _outlineRenderer.sprite = _config.OutlineSprite;
        _outlineRenderer.color = _config.OutlineColor;
        _outlineRenderer.sortingOrder = _config.SortingOrder;

        _arrowRenderer.sprite = _config.ArrowSprite;
        _arrowRenderer.color = _config.ArrowColor;

        _arrowOffset.transform.localPosition = _config.ArrowOffset;

        transform.SetParent(parent);
        transform.localPosition = _config.Offset;
        transform.localScale = _config.Size;
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
            transform.localPosition = _config.Offset;
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
        _outlineRenderer.color = value ? _config.OutlineColor : _highlightedOutlineColor;
    }

    public void OnSpawned(OutlineConfig config, Transform parent, IMemoryPool pool)
    {
        _pool = pool;

        gameObject.SetActive(true);
        Initialize(config, parent);
    }

    public void OnDespawned()
    {
        _config = null;
        transform.parent = null;

        gameObject.SetActive(false);
    }

    public class Factory : PlaceholderFactory<OutlineConfig, Transform, OutlineView> { }
}
