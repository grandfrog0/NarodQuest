using System.Linq;
using UnityEngine;

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

    [SerializeField] private PlayerMovementController _player;
    [SerializeField] private BringableObjectController _bringableController;
    [SerializeField] private InventoryManager _inventory;
    [SerializeField] private CinemachineCameraController _cinemachineCameraController;
    [SerializeField] private CameraConstraintsRegistrator _cameraConstraintsRegistrator;
    [SerializeField] private DroppedItemsRegistrator _droppedItemsRegistrator;
    [SerializeField] private DescriptablesRegistrator _descriptablesRegistrator;
    [SerializeField] private QuestsRegistrator _questsRegistrator;
    [SerializeField] private StateObjectsRegistrator _stateObjectsRegistrator;

    public PlayerSave CreatePlayerSave()
    {
        return new PlayerSave
        {
            Position = _player.transform.position,
            Axis = _player.Axis,
            Inventory = _inventory.CreateSerializable(),
            CurrentBringableId = _bringableController.Current?.GetId() ?? -1,
            CameraConstraints = _cinemachineCameraController.GetConstraints()?.Name ?? "",
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

        _player.transform.position = playerSave.Position;
        _player.Axis = playerSave.Axis;

        _inventory.ApplySerializable(playerSave.Inventory);

        GameObject bringable = ObjectRegistrator.Get(playerSave.CurrentBringableId);
        if (bringable != null)
        {
            _bringableController.Bring(bringable.GetComponent<PlayerBringable>());
        }

        CameraConstraints constraints = _cameraConstraintsRegistrator.GetByName(playerSave.CameraConstraints);
        _cinemachineCameraController.SetConstraints(constraints);
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
