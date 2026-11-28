using UnityEngine;

public class PlayerSave
{
    // 1. Игрок (координаты, инвентарь, предмет в руке, локация)

    public Vector3 Position;
    public Vector2 Axis;
    public InventorySerializable Inventory;
    public int CurrentBringableId;
    public string LocationId;
}

