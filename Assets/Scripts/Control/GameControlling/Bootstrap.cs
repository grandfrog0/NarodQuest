using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private SaveManager _saveManager;
    private CinemachineCameraController _cinemachineCameraController;
    private LocatorManager _locatorManager;
    private InteractablesSelector _interactablesSelector;
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
        InteractablesSelector interactablesSelector,
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
        _interactablesSelector = interactablesSelector;
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

        _cinemachineCameraController.Initialize();

        // Графика
        _locatorManager.Initialize();

        // Игрок
        _bringableController.Initialize();

        // Загрузка данных из сохранения
        _saveManager.Load();

        // Игрок
        _interactablesSelector.Initialize();
            
        // Другое
        _questsRegistrator.Initialize();
    }

    private void OnApplicationQuit()
    {
        _saveManager.Save();
    }
}
