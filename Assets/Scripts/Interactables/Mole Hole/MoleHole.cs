using System;
using UnityEngine;

public class MoleHole : InteractableObject
{
    private Action _onInteract;
    public MoleHoleState HoleState
    {
        get => _holeState;
        set
        {
            _holeState = value;
            ShowHoleType(value);
        }
    }
    private MoleHoleState _holeState;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        HoleState = MoleHoleState.Grassed;
        IsActive = false;
    }

    private void ShowHoleType(MoleHoleState holeType)
    {
        _renderer.color = holeType switch
        {
            MoleHoleState.Grassed => Color.green,
            MoleHoleState.Hole => Color.black,
            MoleHoleState.MoleHole => Color.brown,
            MoleHoleState.Rock => Color.gray,
            _ => Color.white
        };
    }

    public void Subscribe(Action onInteract)
    {
        _onInteract = onInteract;
        IsActive = false;
    }
    public void Describe()
    {
        _onInteract = null;
        IsActive = true;
    }
    public override void Interact()
    {
        _onInteract?.Invoke();
    }
}
