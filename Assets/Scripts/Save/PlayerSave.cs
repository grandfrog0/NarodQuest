using UnityEngine;

public class PlayerSave
{
    // 1. Игрок (координаты, инвентарь, предмет в руке)

    public Vector3 Position;
    public Vector2 Axis;
    public InventoryConfig Inventory;
    public int CurrentBringableId;

    public PlayerBringable GetCurrentBringable()
    {
        return ObjectRegistrator.Get(CurrentBringableId).GetComponent<PlayerBringable>();
    }
}

