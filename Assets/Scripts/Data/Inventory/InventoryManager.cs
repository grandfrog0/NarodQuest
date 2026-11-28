using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class InventoryManager
{
    public UnityEvent OnChanged { get; } = new();
    public UnityEvent<int> OnSelectionChanged { get; } = new();
    private InventoryData _inventory;

    public bool IsEmpty => _inventory.Items.Count == 0;

    private ItemRegistrator _itemRegistrator;

    [Inject]
    private void Construct(ItemRegistrator itemRegistrator, InventoryData inventory)
    {
        _itemRegistrator = itemRegistrator;
        _inventory = inventory;
    }

    public InventorySerializable CreateSerializable()
    {
        return _inventory.CreateSerializable();
    }

    public void ApplySerializable(InventorySerializable serializable)
    {
        _inventory.ApplySerializable(_itemRegistrator.GetByName, serializable);
        OnChanged.Invoke();

        OnSelectionChanged.Invoke(_inventory.SelectedIndex);
    }

    public void SelectItem(int index)
    {
        _inventory.SelectedIndex = index;
        OnSelectionChanged.Invoke(_inventory.SelectedIndex);
    }

    public int GetCount(Item item)
    {
        return _inventory.Items.FirstOrDefault(x => x.Item == item)?.Count ?? 0;
    }
    private bool TryGetPair(Item item, out ItemCountPair pair)
    {
        return (pair = _inventory.FirstOrDefault(x => x.Item == item)) != null;
    }

    public void Add(Item item, int count)
    {
        if (count <= 0)
        {
            return;
        }

        if (TryGetPair(item, out ItemCountPair pair))
        {
            pair.Count += count;
        }
        else
        {
            _inventory.Items.Add(new ItemCountPair(item, count));
        }
        OnChanged.Invoke();
    }
    public void Remove(Item item, int count)
    {
        if (count <= 0)
        {
            return;
        }

        if (TryGetPair(item, out ItemCountPair pair))
        {
            pair.Count -= count;

            if (pair.Count <= 0)
            {
                RemovePair(pair);
            }
            OnChanged.Invoke();
        }
    }
    public void Remove(Item item)
    {
        if (TryGetPair(item, out ItemCountPair pair))
        {
            RemovePair(pair);
            OnChanged.Invoke();
        }
    }
    private void RemovePair(ItemCountPair pair)
    {
        _inventory.Items.Remove(pair);
    }

    public void Clear()
    {
        _inventory.Items.Clear();
        OnChanged.Invoke();
        SelectItem(-1);
    }

    public IEnumerable<ItemCountPair> GetEnumerator()
    {
        return _inventory;
    }
}
