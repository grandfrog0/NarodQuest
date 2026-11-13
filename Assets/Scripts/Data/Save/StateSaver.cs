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

    public PlayerSave PlayerSave { get; private set; }
    public EnvironmentDto EnvironmentSave { get; private set; }
    public QuestSave QuestSave { get; private set; }

    private PlayerMovementController _playerMove;
    private BringableObjectController _bringableController;
    private InventoryManager _inventory;
    private CinemachineCameraController _cameraController;
    private CameraConstraintsRegistrator _cameraConstraintsRegistrator;
    private DroppedItemsRegistrator _droppedItemsRegistrator;
    private DescriptablesRegistrator _descriptablesRegistrator;
    private QuestsRegistrator _questsRegistrator;
    private StateObjectsRegistrator _stateObjectsRegistrator;
    private ObjectRegistrator _objectRegistrator;

    [Inject]
    private void Construct(
        PlayerMovementController playerMove, 
        BringableObjectController bringableController, 
        InventoryManager inventory, 
        CinemachineCameraController cameraController, 
        CameraConstraintsRegistrator cameraConstraintsRegistrator,
        DroppedItemsRegistrator droppedItemsRegistrator, 
        DescriptablesRegistrator descriptablesRegistrator,
        QuestsRegistrator questsRegistrator, 
        StateObjectsRegistrator stateObjectsRegistrator,
        ObjectRegistrator objectRegistrator
        )
    {
        _playerMove = playerMove;
        _bringableController = bringableController;
        _inventory = inventory;
        _cameraController = cameraController;
        _cameraConstraintsRegistrator = cameraConstraintsRegistrator;
        _droppedItemsRegistrator = droppedItemsRegistrator;
        _descriptablesRegistrator = descriptablesRegistrator;
        _questsRegistrator = questsRegistrator;
        _stateObjectsRegistrator = stateObjectsRegistrator;
        _objectRegistrator = objectRegistrator;
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
        return new QuestSave
        {
            Quests = _questsRegistrator.Quests.Select(x => new QuestData { Name = x.Name, Tags = x.GetTags() }).ToList()
        };
    }

    public void ApplyPlayerSave(PlayerSave playerSave)
    {
        PlayerSave = playerSave;

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
        EnvironmentSave = environmentSave;

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
        QuestSave = questSave;

        foreach (QuestData data in questSave.Quests)
        {
            AbstractQuest quest = _questsRegistrator.Quests.First(x => x.Name == data.Name);
            quest.ApplyTags(data.Tags);
        }
    }
}
