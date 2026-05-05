using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private UnityEvent _onChanged;
    [SerializeField] private UnityEvent<Item> _onSelectionChanged;
    [SerializeField] private InventoryConfig _config;

    public void SelectItem(Item item)
    {
        _config.SelectedItem = item;
        _onSelectionChanged.Invoke(_config.SelectedItem);
    }

    public int GetCount(Item item)
    {
        return _config.Items.FirstOrDefault(x => x.Item == item)?.Count ?? 0;
    }
    private bool TryGetPair(Item item, out ItemCountPair pair)
    {
        return (pair = _config.FirstOrDefault(x => x.Item == item)) != null;
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
        }
    }
    public void Remove(Item item)
    {
        if (TryGetPair(item, out ItemCountPair pair))
        {
            RemovePair(pair);
            _onChanged.Invoke();
        }
    }
    private void RemovePair(ItemCountPair pair)
    {
        _config.Items.Remove(pair);
    }
    public void Clear()
    {
        _config.Items.Clear();
        _onChanged.Invoke();
        SelectItem(null);
    }

    public IEnumerable<ItemCountPair> GetEnumerator()
    {
        return _config;
    }
}
