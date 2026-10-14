using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LocatorManager _locatorManager;
    [SerializeField] private TransitionManager _transitionManager;
    [SerializeField] private ObjectRegistrator _objectRegistrator;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private InventoryVisualizer _inventoryVisualizer;
    [SerializeField] private ObjectSelector _objectSelector;
    [SerializeField] private BringableObjectController _bringableController;

    private void Start()
    {
        // Сохранение
        _objectRegistrator.Initialize();

        // Инвентарь
        InventoryConfig inventory = _inventoryManager.Initialize(new InventoryConfig());
        _inventoryVisualizer.Initialize();

        // Графика
        _locatorManager.Initialize();
        _transitionManager.Initialize();

        // Игрок
        _bringableController.Initialize();
        _objectSelector.Initialize(inventory, _bringableController);
    }
}
