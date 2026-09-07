using UnityEngine;

/// <summary>
/// Объект, контролирующий поведение PlayerBringable
/// </summary>
[RequireComponent(typeof(PlayerBringable))]
public class TakeableObject : InteractableObject
{
    private Collider2D _collider;

    public PlayerBringable Bringable => _bringable;
    private PlayerBringable _bringable;

    public override void Interact()
    {
        bool isTaken = BringableObjectController.Instance.SwitchBring(_bringable);
        _collider.isTrigger = isTaken;
    }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _bringable = GetComponent<PlayerBringable>();
    }
}
