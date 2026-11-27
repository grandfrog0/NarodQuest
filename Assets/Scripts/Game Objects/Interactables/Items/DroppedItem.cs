using System;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppedItem : InteractableObject
{
    public event Action<ItemCountPair> OnPicked;
    public event Action<ItemCountPair> OnUnpicked;

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private ItemCountPair _drop;

    public bool IsPicked { get; private set; }
    public override bool IsActive { get => base.IsActive && !IsPicked; set => base.IsActive = value; }

    public void Initialize(ItemCountPair item)
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        
        _renderer.sprite = item.Item.Icon;
        OnPicked += x => FindAnyObjectByType<InventoryManager>().Add(x.Item, x.Count);

        IsPicked = false;
    }

    public bool TryPickUp(out ItemCountPair drop)
    {
        if (IsPicked)
        {
            drop = null;
            return false;
        }

        drop = PickUp();
        return true;
    }

    public bool TryUnpick()
    {
        if (!IsPicked)
        {
            return false;
        }

        Unpick();
        return true;
    }

    public void MarkPickedUp(bool value)
    {
        IsPicked = value;
        gameObject.SetActive(!IsPicked);
    }

    private ItemCountPair PickUp()
    {
        IsPicked = true;
        OnPicked?.Invoke(_drop);
        gameObject.SetActive(false);

        return _drop;
    }

    private void Unpick()
    {
        IsPicked = false;
        OnUnpicked?.Invoke(_drop);
        gameObject.SetActive(true);
    }

    private void Start()
    {
        Initialize(_drop);
    }

    public override void Interact(InteractionData data)
    {
        TryPickUp(out _);
    }
}
