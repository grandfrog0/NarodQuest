using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryVisualizer : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ItemView _itemViewPrefab;
    [SerializeField] private RectTransform _selectionMark;
    private List<ItemView> _itemViews = new();

    [SerializeField] private SpriteRenderer _selectedRenderer;

    public void Refresh()
    {
        Clear();
        foreach (ItemCountPair pair in _inventoryManager.GetEnumerator())
        {
            ItemView itemView = Instantiate(_itemViewPrefab, _itemsParent);
            itemView.Initialize(pair.Item.Icon, pair.Count, () => OnSelectionChanged(itemView, pair));
            _itemViews.Add(itemView);
        }
    }

    private void OnSelectionChanged(ItemView itemView, ItemCountPair pair)
    {
        _selectionMark.gameObject.SetActive(true);
        _selectionMark.transform.position = itemView.transform.position;
        _inventoryManager.SelectItem(pair.Item);

        _selectedRenderer.gameObject.SetActive(pair?.Item != null);
        _selectedRenderer.sprite = pair?.Item.Icon;
    }

    public void Clear()
    {
        foreach (ItemView itemView in _itemViews)
        {
            Destroy(itemView.gameObject);
        }
        _itemViews.Clear();
    }

    private void Start()
    {
        Refresh();
    }
}
