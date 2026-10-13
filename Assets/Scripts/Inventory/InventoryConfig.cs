using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[CreateAssetMenu(fileName = "inventory", menuName = "SO/Inventory/Inventory Config")]
public class InventoryConfig : IEnumerable<ItemCountPair>
{
    public List<ItemCountPair> Items = new();
    public Item SelectedItem;

    public InventorySerializable CreateSerializable()
    {
        return new InventorySerializable
        {
            Items = Items.Select(x => new InventorySerializable.ItemPairSerializable { Name = x.Item.Name, Count = x.Count }).ToList(),
            SelectedItem = SelectedItem.Name
        };
    }

    public void ApplySerializable(InventorySerializable serializable)
    {
        // TODO:
        // СДЕЛАТЬ МЕНЕДЖЕР ПРЕДМЕТОВ
    }

    public IEnumerator<ItemCountPair> GetEnumerator() => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
