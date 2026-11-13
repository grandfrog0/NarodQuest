using System;
using System.Collections.Generic;

[Serializable]
public class InventorySerializable
{
    public List<ItemPairSerializable> Items = new();
    public int SelectedIndex = -1;

    [Serializable]
    public class ItemPairSerializable
    {
        public string Name;
        public int Count;
    }
}
