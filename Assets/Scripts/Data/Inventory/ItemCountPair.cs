using System;
using UnityEngine;

[Serializable]
public class ItemCountPair
{
    public Item Item;
    public int Count = 1;

    public ItemCountPair(Item item, int count = 1)
    {
        Item = item;
        Count = count;
    }
}
