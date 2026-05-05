using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "inventory", menuName = "SO/Inventory/Inventory Config")]
public class InventoryConfig : ScriptableObject, IEnumerable<ItemCountPair>
{
    public List<ItemCountPair> Items;
    public Item SelectedItem;

    public IEnumerator<ItemCountPair> GetEnumerator() => Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
