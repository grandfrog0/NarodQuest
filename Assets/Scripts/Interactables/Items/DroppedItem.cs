using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppedItem : InteractableObject
{
    public event Action<ItemCountPair> OnPicked;

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private ItemCountPair _drop;

    public bool IsPicked { get; private set; }

    public void Initialize(ItemCountPair item)
    {
        _renderer.sprite = item.Item.Icon;
        OnPicked = x => FindAnyObjectByType<InventoryManager>().Add(x.Item, x.Count);

        IsPicked = false;
    }

    public ItemCountPair PickUp()
    {
        IsPicked = true;
        OnPicked?.Invoke(_drop);

        gameObject.SetActive(false);

        return _drop;
    }

    private void Start()
    {
        Initialize(_drop);
    }

    public override void Interact(InteractionData data)
    {
        PickUp();
    }
}
