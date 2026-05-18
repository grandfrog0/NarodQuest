using System.Collections.Generic;
using UnityEngine;

public class InventoryVisualizer : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ItemView _itemViewPrefab;
    [SerializeField] private RectTransform _selectionMark;
    private List<ItemView> _itemViews = new();

    [SerializeField] private PlayerItemViewer _playerItemViewer;

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
        Debug.Log(_selectionMark);
        if (pair != null && _inventoryManager.SelectedItem != pair.Item)
        {
            _selectionMark.gameObject.SetActive(true);
            _selectionMark.transform.position = itemView.transform.position;
            _inventoryManager.SelectedItem = pair.Item;

            if (pair?.Item != null)
            {
                _playerItemViewer.Show(pair.Item.Icon);
            }
        }
        else
        {
            _selectionMark.gameObject.SetActive(false);
            _inventoryManager.SelectedItem = null;

            _playerItemViewer.Hide();
        }
        Debug.Log(_selectionMark);
    }

    public void Clear()
    {
        foreach (ItemView itemView in _itemViews)
        {
            Destroy(itemView.gameObject);
        }
        _itemViews.Clear();
    }
    private void OnAnyTakeableTaken(bool value)
    {
        if (value)
        {
            OnSelectionChanged(null, null);
        }
    }

    private void Start()
    {
        Refresh();
    }
    private void OnEnable()
    {
        TakeableObjects.OnAnyTaken += OnAnyTakeableTaken;
    }
    private void OnDisable()
    {
        TakeableObjects.OnAnyTaken -= OnAnyTakeableTaken;
    }
}
