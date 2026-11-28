using System.Linq;
using UnityEngine;
using Zenject;

public class StateSaver : MonoBehaviour
{
    /*
        0. Файлы сохранения:
            1. Игрок (координаты, инвентарь, предмет в руке, ограничения камеры)
            2. Окружение (поднятые предметы, интерактив, нпс, мини-игры)
            3. Квесты (квесты)

            1. Координаты игрока, поворот
            2. Предмет в руках у игрока
            3. Инвентарь
            4. Подобранность каждого инвентарного предмета
            5. Координаты каждого переносимого предмета
            6. Состояние интерактива
            7. Состояние мини-игр
            8. Состояние квестов
            9. Состояние НПС
            10. Ограничения камеры
    */

    private PlayerMovementController _playerMove;
    private BringableObjectController _bringableController;
    private InventoryManager _inventory;
    private CinemachineCameraController _cameraController;
    private CameraConstraintsRegistrator _cameraConstraintsRegistrator;
    private DroppedItemsRegistrator _droppedItemsRegistrator;
    private DescriptablesRegistrator _descriptablesRegistrator;
    private StateObjectsRegistrator _stateObjectsRegistrator;
    private ObjectRegistrator _objectRegistrator;
    private QuestStateHandler _questStateHandler;

    [Inject]
    private void Construct(
        PlayerMovementController playerMove, 
        BringableObjectController bringableController, 
        InventoryManager inventory, 
        CinemachineCameraController cameraController, 
        CameraConstraintsRegistrator cameraConstraintsRegistrator,
        DroppedItemsRegistrator droppedItemsRegistrator, 
        DescriptablesRegistrator descriptablesRegistrator,
        StateObjectsRegistrator stateObjectsRegistrator,
        ObjectRegistrator objectRegistrator,
        QuestStateHandler questStateHandler
        )
    {
        _playerMove = playerMove;
        _bringableController = bringableController;
        _inventory = inventory;
        _cameraController = cameraController;
        _cameraConstraintsRegistrator = cameraConstraintsRegistrator;
        _droppedItemsRegistrator = droppedItemsRegistrator;
        _descriptablesRegistrator = descriptablesRegistrator;
        _stateObjectsRegistrator = stateObjectsRegistrator;
        _objectRegistrator = objectRegistrator;
        _questStateHandler = questStateHandler;
    }

    public PlayerSave CreatePlayerSave()
    {
        return new PlayerSave
        {
            Position = _playerMove.transform.position,
            Axis = _playerMove.Axis,
            Inventory = _inventory.CreateSerializable(),
            CurrentBringableId = _bringableController.Current?.GetId() ?? -1,
            CameraConstraints = _cameraController.GetConstraints()?.Name ?? "",
        };
    }
    public EnvironmentDto CreateEnvironmentSave()
    {
        return new EnvironmentDto
        {
            RaisedObjectsIds = _droppedItemsRegistrator.DroppedItems.Where(x => x.IsPicked).Select(x => x.GetId()).ToList(),
            InteractiveDatas = _descriptablesRegistrator.Descriptables.Select(x => new InteractiveDto { Id = x.Id, Tags = x.GetTags() }).ToList(),
            StateObjects = _stateObjectsRegistrator.ToDtos(),
        };
    }
    public QuestSave CreateQuestSave()
    {
        return _questStateHandler.CreateSave();
    }

    public void ApplyPlayerSave(PlayerSave playerSave)
    {
        _playerMove.transform.position = playerSave.Position;
        _playerMove.Axis = playerSave.Axis;

        _inventory.ApplySerializable(playerSave.Inventory);

        GameObject bringable = _objectRegistrator.Get(playerSave.CurrentBringableId);
        if (bringable != null)
        {
            _bringableController.Bring(bringable.GetComponent<PlayerBringable>());
        }

        CameraConstraints constraints = _cameraConstraintsRegistrator.GetByName(playerSave.CameraConstraints);
        _cameraController.SetConstraints(constraints);
    }
    public void ApplyEnvironmentSave(EnvironmentDto environmentSave)
    {
        foreach (int itemId in environmentSave.RaisedObjectsIds)
        {
            DroppedItem item = _droppedItemsRegistrator.DroppedItems.First(x => x.GetId() == itemId);
            item.MarkPickedUp(true);
        }

        foreach (InteractiveDto data in environmentSave.InteractiveDatas)
        {
            IDescriptable d = _descriptablesRegistrator.Descriptables.First(x => x.Id == data.Id);
            d.ApplyTags(data.Tags);
        }

        _stateObjectsRegistrator.FromDtos(environmentSave.StateObjects);
    }
    public void ApplyQuestSave(QuestSave questSave)
    {
        _questStateHandler.ApplySave(questSave);
    }
}
