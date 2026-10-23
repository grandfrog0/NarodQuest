using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[CreateAssetMenu(fileName = "inventory", menuName = "SO/Inventory/Inventory Config")]
public class InventoryConfig : IEnumerable<ItemCountPair>
{
    public List<ItemCountPair> Items = new();
    public int SelectedIndex = -1;

    public InventorySerializable CreateSerializable()
    {
        return new InventorySerializable
        {
            Items = Items.Select(x => new InventorySerializable.ItemPairSerializable { Name = x.Item.Name, Count = x.Count }).ToList(),
            SelectedIndex = SelectedIndex
        };
    }

    public void ApplySerializable(Func<string, Item> convertItem, InventorySerializable serializable)
    {
        Items = new();

        foreach (var itemPair in serializable.Items)
        {
            Items.Add(new ItemCountPair(convertItem(itemPair.Name), itemPair.Count));
        }

        SelectedIndex = serializable.SelectedIndex;
    }

    public IEnumerator<ItemCountPair> GetEnumerator() => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
