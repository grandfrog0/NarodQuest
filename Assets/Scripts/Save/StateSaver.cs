using System;
using UnityEngine;

public class StateSaver : MonoBehaviour
{
    /*
    0. Файлы сохранения:
    1. Игрок (координаты, инвентарь, предмет в руке)
    2. Окружение (поднятые предметы, интерактив, нпс)
    2. Квесты (мини-игры, квесты)

    1. Координаты игрока, поворот
    2. Предмет в руках у игрока
    3. Инвентарь
    4. Координаты каждого лежащего предмета и его подобранность
    5. Координаты каждого переносимого предмета
    6. Состояние интерактива:
    - Калитки
    7. Состояние мини-игр
    8. Состояние квестов
    8. Состояние НПС
    */

    [SerializeField] private PlayerMovementController _player;
    [SerializeField] private BringableObjectController _bringableController;
    [SerializeField] private InventoryManager _inventory;

    public PlayerSave CreatePlayerSave()
    {
        return new PlayerSave
        {
            Position = _player.transform.position,
            Axis = _player.Axis,
            Inventory = _inventory.Clone(),
            CurrentBringableId = _bringableController.Current.GetId()
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
        _inventory.Initialize(playerSave.Inventory);
        _bringableController.Bring(playerSave.GetCurrentBringable());
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
