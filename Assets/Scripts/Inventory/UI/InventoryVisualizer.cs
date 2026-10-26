using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryVisualizer : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ItemView _itemViewPrefab;
    [SerializeField] private RectTransform _selectionMark;
    [SerializeField] private RectTransform _horizontalLayoutGroup;
    private List<ItemView> _itemViews = new();

    [SerializeField] private SpriteRenderer _selectedRenderer;

    public bool IsHidden
    {
        get => _isHidden;
        set
        {
            _isHidden = value;
            RefreshVisibility();
        }
    }
    [SerializeField] private bool _isHidden;

    public void Refresh()
    {
        Clear();

        int i = 0;
        foreach (ItemCountPair pair in _inventoryManager.GetEnumerator())
        {
            int a = i;

            ItemView itemView = Instantiate(_itemViewPrefab, _itemsParent);
            itemView.Initialize(pair, () => OnItemViewClick(a));
            _itemViews.Add(itemView);

            i++;
        }

        RefreshVisibility();
    }

    private void RefreshVisibility()
    {
        gameObject.SetActive(!_isHidden && !_inventoryManager.IsEmpty);
    }

    private void OnItemViewClick(int index)
    {
        _inventoryManager.SelectItem(index);
    }

    public void OnSelectionChanged(int selectedIndex)
    {
        if (selectedIndex == -1)
        {
            _selectionMark.gameObject.SetActive(false);
            _selectedRenderer.gameObject.SetActive(false);
            return;
        }

        ItemView itemView = _itemViews[selectedIndex];

        _selectionMark.gameObject.SetActive(true);
        _selectionMark.transform.position = itemView.transform.position;

        _selectedRenderer.gameObject.SetActive(itemView.ItemCountPair.Item != null);
        _selectedRenderer.sprite = itemView.ItemCountPair.Item.Icon;
    }

    public void Clear()
    {
        foreach (ItemView itemView in _itemViews)
        {
            Destroy(itemView.gameObject);
        }
        _itemViews.Clear();
    }

    public void Initialize()
    {
        _inventoryManager.OnChanged.AddListener(Refresh);
        _inventoryManager.OnSelectionChanged.AddListener(OnSelectionChanged);
    }
}
