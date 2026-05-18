using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    public UnityEvent<Item, int> OnItemCountChanged { get; } = new();
    [SerializeField] private UnityEvent _onChanged;
    [SerializeField] private UnityEvent<Item> _onSelectionChanged;
    [SerializeField] private InventoryConfig _config;

    public Item SelectedItem
    {
        get => _config.SelectedItem;
        set
        {
            _config.SelectedItem = value;
            _onSelectionChanged.Invoke(_config.SelectedItem);
        }
    }

    public int GetCount(Item item)
    {
        return _config.Items.FirstOrDefault(x => x.Item == item)?.Count ?? 0;
    }
    private bool TryGetPair(Item item, out ItemCountPair pair)
    {
        return (pair = _config.FirstOrDefault(x => x.Item == item)) != null;
    }
    public bool HasItem(Item item)
    {
        return GetCount(item) > 0;
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
            _config.Items.Add(new ItemCountPair(item, count));
        }
        _onChanged.Invoke();
        OnItemCountChanged.Invoke(item, pair?.Count ?? count);
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
            _onChanged.Invoke();
            OnItemCountChanged.Invoke(item, pair.Count >= 0 ? pair.Count : 0);
        }
    }
    public void Remove(Item item)
    {
        if (TryGetPair(item, out ItemCountPair pair))
        {
            RemovePair(pair);
            _onChanged.Invoke();
            OnItemCountChanged.Invoke(item, 0);
        }
    }
    private void RemovePair(ItemCountPair pair)
    {
        _config.Items.Remove(pair);
    }
    public void Clear()
    {
        foreach (ItemCountPair pair in _config.Items)
        {
            OnItemCountChanged.Invoke(pair.Item, 0);
        }
        _config.Items.Clear();
        _onChanged.Invoke();
        SelectedItem = null;
    }

    public IEnumerable<ItemCountPair> GetEnumerator()
    {
        return _config;
    }
}
