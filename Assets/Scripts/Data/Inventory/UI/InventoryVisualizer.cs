using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject _viewPanel;
    [SerializeField] private RectTransform _itemsParent;
    [SerializeField] private ItemView _itemViewPrefab;
    [SerializeField] private RectTransform _selectionMark;
    [SerializeField] private SpriteRenderer _selectedRenderer;
    private InventoryManager _inventoryManager;
    private List<ItemView> _itemViews = new();

    [SerializeField] private bool _isHidden;
    public bool IsHidden
    {
        get => _isHidden;
        set
        {
            _isHidden = value;
            RefreshVisibility();
        }
    }

    [Inject]
    private void Construct(InventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
        _inventoryManager.OnChanged.AddListener(Refresh);
        _inventoryManager.OnSelectionChanged.AddListener(OnSelectionChanged);
    }

    private void Start()
    {
        RefreshVisibility();
    }

    public void Refresh()
    {
        Clear();

        RefreshVisibility();

        int i = 0;
        foreach (ItemCountPair pair in _inventoryManager.GetEnumerator())
        {
            int a = i;

            ItemView itemView = Instantiate(_itemViewPrefab, _itemsParent);
            itemView.Initialize(pair, () => OnItemViewClick(a));
            _itemViews.Add(itemView);

            i++;
        }
    }

    private void RefreshVisibility()
    {
        _viewPanel.SetActive(!_isHidden && !_inventoryManager.IsEmpty);
    }

    private void OnItemViewClick(int index)
    {
        _inventoryManager.SelectItem(index);
    }

    public void OnSelectionChanged(int selectedIndex)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_itemsParent);

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

    private void OnDestroy()
    {
        _inventoryManager.OnChanged.RemoveListener(Refresh);
        _inventoryManager.OnSelectionChanged.RemoveListener(OnSelectionChanged);
    }
}
