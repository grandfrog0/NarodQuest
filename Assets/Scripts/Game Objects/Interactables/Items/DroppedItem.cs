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
        //OnPicked += x => FindAnyObjectByType<InventoryManager>().Add(x.Item, x.Count);

        IsPicked = false;
    }

    public bool TryPickUp(Action<Item, int> pickAction)
    {
        if (IsPicked)
        {
            return false;
        }

        PickUp(pickAction);
        return true;
    }

    public bool TryUnpick(Action<Item, int> unpickAction)
    {
        if (!IsPicked)
        {
            return false;
        }

        Unpick(unpickAction);
        return true;
    }

    public void MarkPickedUp(bool value)
    {
        IsPicked = value;
        gameObject.SetActive(!IsPicked);
    }

    private ItemCountPair PickUp(Action<Item, int> pickAction)
    {
        IsPicked = true;

        pickAction?.Invoke(_drop.Item, _drop.Count);
        OnPicked?.Invoke(_drop);

        gameObject.SetActive(false);

        return _drop;
    }

    private void Unpick(Action<Item, int> unpickAction)
    {
        IsPicked = false;
        gameObject.SetActive(true);

        unpickAction?.Invoke(_drop.Item, _drop.Count);
        OnUnpicked?.Invoke(_drop);
    }

    private void Start()
    {
        Initialize(_drop);
    }

    protected override void HandleInteraction(InteractionData data)
    {
        TryPickUp(data.InventoryManager.Add);
    }
}
