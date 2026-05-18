using System;
using UnityEngine;

public class TakeableObjects : InteractableObject
{
    public static event Action<bool> OnAnyTaken;

    private Collider2D _collider;
    public bool IsTaken { get; private set; } = false;
    private MoveWithPlayerObject _moveWithPlayerObject;

    public override void Interact()
    {
        if (!IsTaken)
        {
            IsTaken = true;
            _collider.isTrigger = true;

            OnAnyTaken?.Invoke(true);
            _moveWithPlayerObject.Subscribe();
            _moveWithPlayerObject.Owner.SpeedMultipler = 0.5f;
        }
        else
        {
            IsTaken = false;
            _collider.isTrigger = false;

            OnAnyTaken?.Invoke(false);
            _moveWithPlayerObject.Owner.SpeedMultipler = 1f;
            _moveWithPlayerObject.Describe();
        }
    }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _moveWithPlayerObject = GetComponent<MoveWithPlayerObject>();
    }
}
