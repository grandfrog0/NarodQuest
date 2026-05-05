using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppedItem : InteractableObject
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private ItemCountPair _drop;
    public Action<ItemCountPair> OnPicked { get; set; }

    public void Initialize(ItemCountPair item)
    {
        _renderer.sprite = item.Item.Icon;
        OnPicked = x => FindAnyObjectByType<InventoryManager>().Add(x.Item, x.Count);
    }

    public ItemCountPair PickUp()
    {
        OnPicked?.Invoke(_drop);
        gameObject.SetActive(false);
        return _drop;
    }

    private void Start()
    {
        Initialize(_drop);
    }

    public override void Interact()
    {
        PickUp();
    }
}
