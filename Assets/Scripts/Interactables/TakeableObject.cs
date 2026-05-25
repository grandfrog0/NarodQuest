using UnityEngine;

[RequireComponent(typeof(PlayerBringable))]
public class TakeableObject : InteractableObject
{
    private Collider2D _collider;
    private PlayerBringable _bringable;

    public override void Interact()
    {
        bool isTaken = BringableObjectController.Instance.SwitchBring(_bringable);
        Debug.Log(isTaken);
        _collider.isTrigger = isTaken;
    }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _bringable = GetComponent<PlayerBringable>();
    }
}
