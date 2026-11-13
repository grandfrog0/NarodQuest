using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "SO/Inventory/ItemRegistrator")]
public class ItemRegistrator : ScriptableObject
{
    [SerializeField] private List<Item> _items;
    private Dictionary<string, Item> _itemPairs;

    public void Initialize()
    {
        _itemPairs = new();
        foreach (Item item in _items)
        {
            _itemPairs[item.Name] = item;
        }
    }

    public Item GetByName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && _itemPairs.TryGetValue(name, out Item value) ? value : null;
    }
}
