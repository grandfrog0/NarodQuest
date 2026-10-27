using UnityEngine;
using static UnityEditor.Progress;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private LocatorManager _locatorManager;
    [SerializeField] private TransitionManager _transitionManager;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private InventoryVisualizer _inventoryVisualizer;
    [SerializeField] private ObjectSelector _objectSelector;
    [SerializeField] private BringableObjectController _bringableController;

    [Header("Registrators")]
    [SerializeField] private ObjectRegistrator _objectRegistrator;
    [SerializeField] private DroppedItemsRegistrator _droppedItemsRegistrator;
    [SerializeField] private DescriptablesRegistrator _descriptablesRegistrator;
    [SerializeField] private ItemRegistrator _itemRegistrator;
    [SerializeField] private CameraConstraintsRegistrator _cameraConstraintsRegistrator;


    private void Start()
    {
        // Регистраторы
        _objectRegistrator.Initialize();
        _droppedItemsRegistrator.Initialize();
        _descriptablesRegistrator.Initialize();
        _itemRegistrator.Initialize();
        _cameraConstraintsRegistrator.Initialize();

        // Инвентарь
        _inventoryVisualizer.Initialize();
        InventoryConfig inventory = _inventoryManager.Initialize(_itemRegistrator);

        _cameraController.Initialize();

        // Графика
        _locatorManager.Initialize();
        _transitionManager.Initialize();

        // Игрок
        _bringableController.Initialize();
        _objectSelector.Initialize(inventory, _bringableController);

        // Загрузка данных из сохранения
        _saveManager.Load();
    }

    private void OnApplicationQuit()
    {
        _saveManager.Save();
    }
}
