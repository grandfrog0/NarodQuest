using System;
using UnityEngine;

public class StateSaver : MonoBehaviour
{
    /*
        0. Файлы сохранения:
            1. Игрок (координаты, инвентарь, предмет в руке, ограничения камеры)
        2. Окружение (поднятые предметы, интерактив, нпс)
        2. Квесты (мини-игры, квесты)

            1. Координаты игрока, поворот
            2. Предмет в руках у игрока
            3. Инвентарь
        4. Подобранность каждого инвентарного предмета
        5. Координаты каждого переносимого предмета
        6. Состояние интерактива:
        - Калитки
        - Стола
        7. Состояние мини-игр
        8. Состояние квестов
        9. Состояние НПС
            10. Ограничения камеры
    */

    [SerializeField] private PlayerMovementController _player;
    [SerializeField] private BringableObjectController _bringableController;
    [SerializeField] private InventoryManager _inventory;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private CameraConstraintsRegistrator _cameraConstraintsRegistrator;

    public PlayerSave CreatePlayerSave()
    {
        return new PlayerSave
        {
            Position = _player.transform.position,
            Axis = _player.Axis,
            Inventory = _inventory.CreateSerializable(),
            CurrentBringableId = _bringableController.Current?.GetId() ?? -1,
            CameraConstraints = _cameraController.GetConstraints()?.name ?? ""
        };
    }
    public EnvironmentSave CreateEnvironmentSave()
    {
        throw new NotImplementedException();
    }
    public QuestSave CreateQuestSave()
    {
        throw new NotImplementedException();
    }

    public void ApplyPlayerSave(PlayerSave playerSave)
    {
        _player.transform.position = playerSave.Position;
        _player.Axis = playerSave.Axis;

        _inventory.ApplySerializable(playerSave.Inventory);

        PlayerBringable bringable = playerSave.GetCurrentBringable();
        if (bringable != null)
        {
            _bringableController.Bring(bringable);
        }

        CameraConstraints constraints = _cameraConstraintsRegistrator.GetByName(playerSave.CameraConstraints);
        _cameraController.SetConstraints(constraints);
    }
    public void ApplyEnvironmentSave(EnvironmentSave environmentSave)
    {
        throw new NotImplementedException();
    }
    public void ApplyQuestSave(QuestSave questSave)
    {
        throw new NotImplementedException();
    }
}
