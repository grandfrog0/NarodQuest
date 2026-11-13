using System;
using UnityEngine;

public class OutlineView : MonoBehaviour, IDisposable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _outlineRenderer;
    [SerializeField] private SpriteRenderer _arrowRenderer;

    private Color _defaultOutlineColor;
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

    public void Initalize(OutlineConfig config)
    {
        _defaultOutlineColor = config.OutlineColor;
        _highlightedOutlineColor = new Color(
            _defaultOutlineColor.r, 
            _defaultOutlineColor.g, 
            _defaultOutlineColor.b, 
            _defaultOutlineColor.a * 0.34f
            );

        _outlineRenderer.sprite = config.OutlineSprite;
        _outlineRenderer.color = _defaultOutlineColor;
        _outlineRenderer.sortingOrder = config.SortingOrder;

        _arrowRenderer.sprite = config.ArrowSprite;
        _arrowRenderer.color = config.ArrowColor;

        transform.localPosition = config.Offset;
        transform.localScale = config.Size;
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
            Destroy(gameObject);
        }
    }

    private void SetDynamic(bool value)
    {
        _arrowRenderer.gameObject.SetActive(value);
        _outlineRenderer.color = value ? _defaultOutlineColor : _highlightedOutlineColor;
    }
}
