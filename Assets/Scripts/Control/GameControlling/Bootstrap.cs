using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private SaveManager _saveManager;
    private CinemachineCameraController _cinemachineCameraController;
    private LocatorManager _locatorManager;
    private InventoryManager _inventoryManager;
    private InventoryVisualizer _inventoryVisualizer;
    private InteractablesSelector _interactablesSelector;
    private Interactor _interactor;
    private BringableObjectController _bringableController;

    private DroppedItemsRegistrator _droppedItemsRegistrator;
    private DescriptablesRegistrator _descriptablesRegistrator;
    private ItemRegistrator _itemRegistrator;
    private QuestsRegistrator _questsRegistrator;

    [Inject]
    private void Construct(
        SaveManager saveManager,
        CinemachineCameraController cinemachineCameraController,
        LocatorManager locatorManager,
        InventoryManager inventoryManager,
        InventoryVisualizer inventoryVisualizer,
        InteractablesSelector interactablesSelector,
        Interactor interactor,
        BringableObjectController bringableController,
        DroppedItemsRegistrator droppedItemsRegistrator,
        DescriptablesRegistrator descriptablesRegistrator,
        ItemRegistrator itemRegistrator,
        QuestsRegistrator questsRegistrator
        )
    {
        _saveManager = saveManager;
        _cinemachineCameraController = cinemachineCameraController;
        _locatorManager = locatorManager;
        _inventoryManager = inventoryManager;
        _inventoryVisualizer = inventoryVisualizer;
        _interactablesSelector = interactablesSelector;
        _interactor = interactor;
        _bringableController = bringableController;
        _droppedItemsRegistrator = droppedItemsRegistrator;
        _descriptablesRegistrator = descriptablesRegistrator;
        _itemRegistrator = itemRegistrator;
        _questsRegistrator = questsRegistrator;
    }

    private void Start()
    {
        // Регистраторы
        _droppedItemsRegistrator.Initialize();
        _descriptablesRegistrator.Initialize();
        _itemRegistrator.Initialize();

        // Инвентарь
        _inventoryVisualizer.Initialize();
        InventoryData inventory = _inventoryManager.Initialize();

        _cinemachineCameraController.Initialize();

        // Графика
        _locatorManager.Initialize();

        // Игрок
        _bringableController.Initialize();

        // Загрузка данных из сохранения
        _saveManager.Load();

        // Игрок
        _interactablesSelector.Initialize();
        _interactor.Initialize(inventory);
            
        // Другое
        _questsRegistrator.Initialize();
    }

    private void OnApplicationQuit()
    {
        _saveManager.Save();
    }
}
