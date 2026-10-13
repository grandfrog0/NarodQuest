using System.Collections.Generic;

public class InventorySerializable
{
    public List<ItemPairSerializable> Items;
    public string SelectedItem;

    public class ItemPairSerializable
    {
        public string Name;
        public int Count;
    }
}
