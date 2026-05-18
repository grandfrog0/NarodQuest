using UnityEngine;

public class CostumeContainer : MonoBehaviour
{
    private InventoryManager _inventoryManager;
    public bool HasCostume
    {
        get => _hasCostume;
        private set
        {
            _hasCostume = value;
            Debug.Log("Has costume: " + _hasCostume);
        }
    }
    private bool _hasCostume = false;

    private void Start()
    {
        _inventoryManager = FindAnyObjectByType<InventoryManager>();
        _inventoryManager.OnItemCountChanged.AddListener(CheckCostume);
    }

    private void CheckCostume(Item item, int count)
    {
        if (item.Name == "Costume")
        {
            HasCostume = count > 0;
        }
    }
}
